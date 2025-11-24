using UnityEngine;

public class PlayerFinalRigidbody : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float runSpeed = 6f;
    
    private Animator anim;
    private Rigidbody rb;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);

    
        float forwardSpeed = Mathf.Max(0, moveZ); 
        float speedValue = forwardSpeed > 0.1f ? (running ? 2f : 1f) : 0f;
        
        anim.SetFloat("Speed", speedValue);
        anim.SetFloat("Strafe", moveX);
        anim.SetBool("IsRunning", running && moveZ > 0);

    
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
          
        }

        // Rotation souris
        float mouseX = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(0, mouseX, 0);
    }

    void FixedUpdate()
    {
        bool running = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = running && Input.GetAxisRaw("Vertical") > 0 ? runSpeed : walkSpeed;

        Vector3 move = transform.forward * Input.GetAxisRaw("Vertical") * currentSpeed;
        move += transform.right * Input.GetAxisRaw("Horizontal") * walkSpeed * 0.8f;
        move.y = rb.linearVelocity.y;
        rb.linearVelocity = move;
    }
}