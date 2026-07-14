using UnityEngine;

public class BotVoice : MonoBehaviour
{
    public Transform player;
    public float distanceToPlay = 12f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float d = Vector3.Distance(transform.position, player.position);

        if (d <= distanceToPlay)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}