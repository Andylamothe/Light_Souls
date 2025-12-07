using UnityEngine;

public class CameraThirdPerson : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Glisse ton JOUEUR ici !

    [Header("Settings")]
    public float mouseSensitivity = 2f;
    public float distance = 5f;
    public Vector2 pitchLimits = new Vector2(-40f, 60f); // Min/Max vertical
    public float followSpeed = 10f;
    public LayerMask clipMask = -1; // Pour éviter les murs

    private float pitch = 0f;
    // private Vector3 offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // offset = transform.localPosition;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Mouse Y : Pitch caméra
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        // Mouse X : Rotation joueur horizontal
        float yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.RotateAround(target.position + Vector3.up * 1.5f, Vector3.up, yaw);


        // Position idéale caméra
        Quaternion rotation = Quaternion.Euler(pitch, target.eulerAngles.y, 0);
        // Vector3 idealPos = target.position - rotation * Vector3.forward * distance + Vector3.up * 1.5f;
        Vector3 idealPos = target.position + Vector3.up * 1.5f - rotation * Vector3.forward * distance;

        // Évite les murs (raycast)
        if (Physics.Linecast(target.position + Vector3.up * 1.5f, idealPos, out RaycastHit hit, clipMask))
        {
            idealPos = hit.point + hit.normal * 0.2f;
        }

        // Suivi fluide
        transform.position = Vector3.Lerp(transform.position, idealPos, followSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);

        // Zoom molette (optionnel)
        distance -= Input.GetAxis("Mouse ScrollWheel") * 2f;
        distance = Mathf.Clamp(distance, 2f, 10f);
    }
}