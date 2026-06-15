using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void GoToLevel2()
    {
        SceneManager.LoadScene("TPS_Level2");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}