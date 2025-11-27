using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    void Start()
    {
        mainMenu.SetActive(true);     // Toujours visible au début
        optionsMenu.SetActive(false); // Toujours caché au début
    }
}