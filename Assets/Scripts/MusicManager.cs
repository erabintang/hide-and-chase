using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            if (PlayerPrefs.GetInt("Music", 1) == 0)
                audioSource.mute = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;

        PlayerPrefs.SetInt("Music", audioSource.mute ? 0 : 1);
    }
}