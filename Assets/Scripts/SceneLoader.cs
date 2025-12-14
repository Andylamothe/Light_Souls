using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image loadingBackground; // L'image de fond à afficher
    public GameObject menuContainer; // Le GameObject qui contient les éléments du menu (pas le background)
    public Slider loadingBar; // La barre de chargement
    public Text loadingText; // Texte du pourcentage (optionnel)
    public float displayDuration = 3f; // Durée minimale d'affichage en secondes

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Masquer les éléments du menu
        if (menuContainer != null)
        {
            menuContainer.SetActive(false);
            Debug.Log("Éléments du menu masqués");
        }
        else
        {
            Debug.LogError("Le conteneur du menu n'est pas assigné!");
        }

        // S'assurer que le background est visible
        if (loadingBackground != null)
        {
            loadingBackground.enabled = true;
            Debug.Log("Background de chargement visible");
        }

        // Initialiser la barre de chargement
        if (loadingBar != null)
        {
            loadingBar.value = 0f;
            loadingBar.gameObject.SetActive(true);
        }

        // Charger la scène en arrière-plan
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        float startTime = Time.time;
        float barFillTime = displayDuration * 0.8f; // Remplir la barre en 80% du temps de display

        // Attendre la durée minimale et que la scène soit chargée
        while (Time.time - startTime < displayDuration || asyncLoad.progress < 0.9f)
        {
            float elapsedTime = Time.time - startTime;
            float barProgress = Mathf.Clamp01(elapsedTime / barFillTime);
            
            // Mettre à jour la barre de chargement (progression lisse)
            if (loadingBar != null)
            {
                loadingBar.value = barProgress;
            }
            
            // Mettre à jour le texte du pourcentage
            if (loadingText != null)
            {
                loadingText.text = Mathf.RoundToInt(barProgress * 100f) + "%";
            }

            Debug.Log($"Chargement (barre): {barProgress * 100f:F1}%");
            yield return null;
        }

        // Remplir complètement la barre
        if (loadingBar != null)
        {
            loadingBar.value = 1f;
        }
        if (loadingText != null)
        {
            loadingText.text = "100%";
        }

        Debug.Log("Chargement terminé, activation de la scène...");

        // Activer la scène
        asyncLoad.allowSceneActivation = true;

        // Attendre que la scène soit vraiment activée
        yield return asyncLoad;

        Debug.Log("Scène chargée!");
    }
}
