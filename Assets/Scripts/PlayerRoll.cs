using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;

    public float rollForce = 10f;
    public float rollDuration = 0.45f;

    private bool isRolling = false;
    private float rollTimer = 0f;
    private Vector3 rollDirection;

    public bool IsRolling() => isRolling;

    void Start()
    {
        if (!anim) anim = GetComponent<Animator>();
        if (!rb) rb = GetComponent<Rigidbody>();
    }

    public void StartRoll(Vector3 dir, int rollIndex)
    {
        if (isRolling) return;

        isRolling = true;
        rollTimer = rollDuration;
        rollDirection = dir.normalized;

        anim.SetInteger("RollDir", rollIndex);
        anim.SetTrigger("Roll");

        Debug.Log("RollDir = " + rollIndex);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }


    void FixedUpdate()
    {
        if (!isRolling) return;

        rollTimer -= Time.fixedDeltaTime;

        rb.linearVelocity = rollDirection * rollForce;

        if (rollTimer <= 0)
        {
            isRolling = false;
        }
    }
}
