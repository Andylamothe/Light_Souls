using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    void Start()
    {
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);     // Toujours visible au début
        }
        else
        {
            Debug.LogError("Main Menu GameObject n'est pas assigné!");
        }

        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false); // Toujours caché au début
        }
        else
        {
            Debug.LogWarning("Options Menu GameObject n'est pas assigné!");
        }
    }
}