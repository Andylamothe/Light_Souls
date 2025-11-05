using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f); // Décalage caméra/joueur
    [SerializeField] private float smoothSpeed = 0.125f; // Fluidité du mouvement

    void LateUpdate()
    {
        if (player == null) return; // Sécurité

        // Position souhaitée
        Vector3 desiredPosition = player.position + offset;

        // Mouvement fluide (interpolation)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Appliquer la position à la caméra
        transform.position = smoothedPosition;

        // (optionnel) Garder la caméra orientée vers le joueur
        // transform.LookAt(player);
    }
}
