using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game")]
    public int totalBot = 3;

    [Header("Timer")]
    public float timer = 540f;

    private int caughtBot = 0;
    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            gameEnded = true;

            SceneManager.LoadScene("mainmenu");
        }
    }

    public void CatchBot()
    {
        if (gameEnded) return;

        caughtBot++;

        if (caughtBot >= totalBot)
        {
            gameEnded = true;

            SceneManager.LoadScene("WinScene");
        }
    }
}