using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraThirdPersonV2 : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 targetOffset = new Vector3(0f, 1.5f, 0f);

    [Header("Controls")]
    public float mouseSensitivityY = 2f;
    public Vector2 pitchLimits = new Vector2(-40f, 60f);

    [Header("Distance & Collision")]
    public float distance = 5f;
    public LayerMask clipMask = ~0;
    public float collisionRadius = 0.25f;
    public float collisionOffset = 0.15f;

    private float pitch = 10f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraThirdPersonV2: target not assigned.");
            enabled = false;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        pitch = transform.eulerAngles.x;
        if (pitch > 180f) pitch -= 360f;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
    }

    void LateUpdate()
    {
        if (target == null) return;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        Vector3 targetPos = target.position + targetOffset;

        float yaw = target.eulerAngles.y;

        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 idealPos = targetPos - camRotation * Vector3.forward * distance;

        Vector3 dir = idealPos - targetPos;
        float idealDist = dir.magnitude;

        if (idealDist > 0.001f)
        {
            dir /= idealDist;
            if (Physics.SphereCast(targetPos, collisionRadius, dir, out RaycastHit hit, idealDist, clipMask, QueryTriggerInteraction.Ignore))
            {
                float placedDist = Mathf.Max(0.05f, hit.distance - collisionOffset);
                idealPos = targetPos + dir * placedDist;
            }
        }

        transform.position = idealPos;
        transform.rotation = camRotation;

        transform.LookAt(targetPos);
    }
}
