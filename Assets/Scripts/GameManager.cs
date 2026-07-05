using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Bot")]
    public int totalBot = 3;
    private int botCaught = 0;

    [Header("Timer")]
    public TMP_Text timerText;
    public float startTime = 540f; // 9 menit

    private float currentTime;
    private bool gameEnded = false;

    [Header("Scene")]
    public string winScene;
    public string loseScene;

    [Header("Pause")]
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = startTime;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        UpdateTimer();
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
            currentTime = 0;

        int minute = Mathf.FloorToInt(currentTime / 60);
        int second = Mathf.FloorToInt(currentTime % 60);

        timerText.text = minute.ToString("00") + ":" + second.ToString("00");

        if (currentTime <= 0)
        {
            gameEnded = true;
            SceneManager.LoadScene(loseScene);
        }
    }

    public void CatchBot()
    {
        botCaught++;

        if (botCaught >= totalBot)
        {
            gameEnded = true;
            SceneManager.LoadScene(winScene);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu(string menuScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuScene);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}