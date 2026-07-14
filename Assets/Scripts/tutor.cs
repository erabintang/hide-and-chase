using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial")]
    public Button tutorialButton1;
    public Button tutorialButton2;

    [Header("Voice")]
    public AudioSource audioSource;

    public AudioClip tutorialVoice1;
    public AudioClip tutorialVoice2;

    void Start()
    {
        Time.timeScale = 0f;

        tutorialButton1.gameObject.SetActive(true);
        tutorialButton2.gameObject.SetActive(false);

        tutorialButton1.onClick.AddListener(Tutorial1);
        tutorialButton2.onClick.AddListener(Tutorial2);
    }

    void Tutorial1()
    {
        tutorialButton1.interactable = false;

        StartCoroutine(PlayTutorial1());
    }

    IEnumerator PlayTutorial1()
    {
        audioSource.clip = tutorialVoice1;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        tutorialButton1.gameObject.SetActive(false);

        tutorialButton2.gameObject.SetActive(true);
        tutorialButton2.interactable = true;
    }

    void Tutorial2()
    {
        tutorialButton2.interactable = false;

        StartCoroutine(PlayTutorial2());
    }

    IEnumerator PlayTutorial2()
    {
        audioSource.clip = tutorialVoice2;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        tutorialButton2.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}