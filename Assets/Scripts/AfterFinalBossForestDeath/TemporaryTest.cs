using UnityEngine;

public class TemporaryTest : MonoBehaviour
{
    public AfterFinalBossForestDeath scriptToCall;

    private bool hasTriggered = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasTriggered && collision.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("Player collision detected, triggering script!");

            if (scriptToCall != null)
            {
                scriptToCall.StartSequence(); // Appelle ton script
            }
        }
    }
}
