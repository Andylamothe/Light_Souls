using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    [Header("Références")]
    public AudioSource audioSource;           // Son à jouer lors de la collision
    public CircularFade circularFade;         // Script pour le cercle fade
    public Transform teleportTarget;          // Position de téléportation

    [Header("Paramètres")]
    public float lockDuration = 2f;           // Temps avant que le fade commence
    public float fadeDuration = 10f;           // Durée du fade
    public bool unlockAfterFade = true;       // Débloquer automatiquement le joueur après téléport
    public Vector3 newScale = Vector3.one;    // Nouvelle échelle du joueur après téléportation

    private bool isColliding = false;
    [SerializeField] private GameObject biomeNeige;
    [SerializeField] private GameObject biomeForest;
    [SerializeField] private PlayerMovementOnly playerMovementOnly;
    
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie que c'est un joueur
        if (!isColliding && other.CompareTag("Player"))
        {
            isColliding = true;
            Debug.Log("Collision avec un joueur détectée !");

            // Bloque le mouvement du joueur
            PlayerMovementOnly movement = other.GetComponent<PlayerMovementOnly>();
            if (movement != null)
                movement.enabled = false;

            // Joue le son
            if (audioSource != null)
                audioSource.Play();

            // Lance la coroutine pour fade + téléport
            StartCoroutine(DelayedTeleport(lockDuration, other));
        }
    }

    private IEnumerator DelayedTeleport(float delay, Collider playerCollider)
    {
        yield return new WaitForSeconds(delay);

        // Lance le fade circulaire et attend sa fin
        if (circularFade != null)
        {
            circularFade.fadeDuration = fadeDuration;
            yield return StartCoroutine(circularFade.FadeToCenterCoroutine());
        }

        // Téléporte le joueur
        if (teleportTarget != null && playerCollider != null)
        {
            biomeNeige.SetActive(true);
           
            playerCollider.transform.position = teleportTarget.position;
            playerCollider.transform.rotation = teleportTarget.rotation;

            // Change la scale du joueur
            playerCollider.transform.localScale = newScale;
            playerMovementOnly.walkSpeed = 2.5f;
            playerMovementOnly.runSpeed = 6;
        }

        // Débloque le mouvement si activé
        if (unlockAfterFade && playerCollider != null)
        {
            PlayerMovementOnly movement = playerCollider.GetComponent<PlayerMovementOnly>();
            if (movement != null)
                movement.enabled = true;
        }

        isColliding = false;
        // desactive la forest a la fin du script
         biomeForest.SetActive(false);
    }
}

public class GameObjet
{
}