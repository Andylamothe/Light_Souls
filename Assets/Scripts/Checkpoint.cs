using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;
    
    // On initialise la position de checkpoint
    void Start()
    {
        // On définit une position de départ par défaut (si aucun checkpoint n'a été touché)
        lastCheckpointPosition = transform.position;
    }

    // Lorsque le joueur touche le checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet est le joueur
        {
            // Sauvegarder la position du checkpoint
            lastCheckpointPosition = transform.position;
            
            Debug.Log("Checkpoint atteint ! Position sauvegardée." +  transform.position);
        }
    }
}
