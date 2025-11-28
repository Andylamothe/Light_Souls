using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircularFade : MonoBehaviour
{
    [Header("Références")]
    public Image fadeCircle;      // Image UI du cercle
    public float fadeDuration = 1f; // Temps pour refermer le cercle

    // Lance le fade depuis l'extérieur vers le centre
    public void StartFade()
    {
        if (fadeCircle != null)
            StartCoroutine(FadeToCenterCoroutine());
    }

    // Coroutine publique pour attendre la fin du fade
    public IEnumerator FadeToCenterCoroutine()
    {
        float timer = 0f;
        fadeCircle.fillAmount = 1f; // Cercle plein au départ

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCircle.fillAmount = Mathf.Clamp01(1f - (timer / fadeDuration));
            yield return null;
        }

        fadeCircle.fillAmount = 0f; // Cercle complètement refermé
    }
}
