using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TPS");
    }

    public void PlayGame2()
    {
        SceneManager.LoadScene("TPS_Level2");
    }
}