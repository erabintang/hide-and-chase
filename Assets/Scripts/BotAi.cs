using UnityEngine;
using UnityEngine.AI;

public class BotAI : MonoBehaviour
{
    [Header("Point Sembunyi")]
    public Transform[] hidePoints;

    [Header("Deteksi Player")]
    public float detectDistance = 8f;
    public float changePointCooldown = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    private float nextChangeTime;
    private int lastPointIndex = -1;
    private bool caught = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;

        GoToRandomPoint();
    }

    private void Update()
    {
        if (caught) return;

        UpdateAnimation();

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectDistance && Time.time >= nextChangeTime)
        {
            GoToRandomPoint();
            nextChangeTime = Time.time + changePointCooldown;
        }
    }

    private void GoToRandomPoint()
    {
        if (hidePoints == null || hidePoints.Length == 0) return;
        if (agent == null) return;

        int randomIndex = Random.Range(0, hidePoints.Length);

        if (hidePoints.Length > 1)
        {
            while (randomIndex == lastPointIndex)
            {
                randomIndex = Random.Range(0, hidePoints.Length);
            }
        }

        lastPointIndex = randomIndex;
        agent.SetDestination(hidePoints[randomIndex].position);
    }

    private void UpdateAnimation()
    {
        if (animator == null || agent == null) return;
    }

    public void Caught()
    {
        if (caught) return;

        caught = true;

        if (GameManager.Instance != null)
            GameManager.Instance.CatchBot();

        Destroy(gameObject);
    }
}