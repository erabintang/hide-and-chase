using UnityEngine;
namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        [Header("Mobile Joystick")]
        public FixedJoystick moveJoystick;
        public VariableJoystick cameraJoystick;

        private float lastForwardTap;
        private float lastJumpTap;
        private float holdForwardTime = 0f;

        private bool sprinting = false;
        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindFirstObjectByType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
        }

        public virtual void MoveInput()
        {
#if UNITY_ANDROID || UNITY_IOS

        cc.input.x = moveJoystick.Horizontal;
        cc.input.z = moveJoystick.Vertical;

#else

            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);

#endif
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            float X;
            float Y;

#if UNITY_ANDROID || UNITY_IOS

X = cameraJoystick.Horizontal;
Y = cameraJoystick.Vertical;

#else

            Y = Input.GetAxis(rotateCameraYInput);
            X = Input.GetAxis(rotateCameraXInput);

#endif

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (moveJoystick != null)
            {
                if (moveJoystick.Vertical > 0.8f)
                {
                    holdForwardTime += Time.deltaTime;

                    if (holdForwardTime >= 2f)
                        sprinting = true;
                }
                else
                {
                    holdForwardTime = 0f;
                    sprinting = false;
                }

                cc.Sprint(sprinting);
            }
            else
            {
                if (Input.GetKeyDown(sprintInput))
                    cc.Sprint(true);
                else if (Input.GetKeyUp(sprintInput))
                    cc.Sprint(false);
            }
        }
        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
#if UNITY_ANDROID || UNITY_IOS

    if (cameraJoystick.Vertical > 0.9f)
    {
        if (Time.time - lastJumpTap < 0.3f)
        {
            if (JumpConditions())
                cc.Jump();
        }

        lastJumpTap = Time.time;
    }

#else

            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();

#endif
        }

        #endregion       
    }
}