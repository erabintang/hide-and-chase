using UnityEngine;

public class CatchTrigger : MonoBehaviour
{
    private BotAI botAI;

    private void Start()
    {
        botAI = GetComponentInParent<BotAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (botAI != null)
            {
                botAI.Caught();
            }
        }
    }
}