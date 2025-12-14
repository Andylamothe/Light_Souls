using UnityEngine;
using System.Collections;

public class AfterFinalBossForestDeath : MonoBehaviour
{
    [Header("Objects to Activate")]
    [SerializeField] private GameObject obj;           
    [SerializeField] private GameObject rock1ToShake;  
    [SerializeField] private GameObject rock2ToShake; 
    [SerializeField] private GameObject redCrystal; 
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float duration = 4f; // Durée avant désactivation

    private bool hasStarted = false;

    public void StartSequence()
    {
        if (hasStarted) return;
        hasStarted = true;

        // Jouer le son
        if (audioSource != null)
            audioSource.Play();

        // Activer les objets
        if (obj != null) obj.SetActive(true);
        if (redCrystal != null) redCrystal.SetActive(true);


        // Lancer la coroutine de désactivation
        StartCoroutine(DeactivateAfterSeconds(duration));
    }

    private IEnumerator DeactivateAfterSeconds(float seconds)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + seconds)
            yield return null;

        // Désactiver les objets après la durée
        if (obj != null) obj.SetActive(false);
        if (rock1ToShake != null) rock1ToShake.SetActive(false);
        if (rock2ToShake != null) rock2ToShake.SetActive(false);
    }
}
