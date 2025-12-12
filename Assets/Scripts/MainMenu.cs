using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;
    private static bool firstLoad = true;
    public void PlayGame()


    {
        
      
        
       
        if (sceneLoader != null)
        {

            sceneLoader.LoadScene("BiomeForet");
        }
        else
        {
            SceneManager.LoadScene("BiomeForet");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    // public void RestartFromCheckpoint()
    // {
    //     // Recharger la scène actuelle pour respawn au checkpoint
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    // }
}
