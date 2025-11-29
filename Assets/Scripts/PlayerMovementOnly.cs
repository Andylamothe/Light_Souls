using UnityEngine;

public class PlayerMovementOnly : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 6f;

    [Header("References")]
    public Camera playerCamera; // Glisse ta Main Camera ici (pour mouvement relatif)
    public ArmHitDetector armHit;
    private Animator anim;
    private Rigidbody rb;
    private bool canAttack = true;
    private float hitCooldown = 0.2f;
    private float lastHitTime = 0;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (playerCamera == null) playerCamera = Camera.main;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);

        // Animation 
        float forwardSpeed = Mathf.Max(0, moveZ);
        float speedValue = forwardSpeed > 0.1f ? (running ? 2f : 1f) : 0f;
        anim.SetFloat("Speed", speedValue);
        anim.SetFloat("Strafe", moveX);
        anim.SetBool("IsRunning", running && moveZ > 0);

        // Attaques 
        if (Input.GetMouseButtonDown(0))
        {
             // cooldown pour le premier clic de sourie. empeche 2 clic super rapide 
            if (Time.time - lastHitTime < hitCooldown) return; // cooldown
           
            // peux attaquer quand l'animation est finie
            if(canAttack) {

                anim.SetTrigger("Attack");
                lastHitTime = Time.time;
            }
            
        }
    }

    void FixedUpdate()
    {
        // Mouvement RELATIF À LA CAMÉRA (top pour third person !)
        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        camForward.y = 0; camRight.y = 0;
        camForward.Normalize(); camRight.Normalize();

        Vector3 move = camForward * Input.GetAxisRaw("Vertical") + camRight * Input.GetAxisRaw("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") > 0 ? runSpeed : walkSpeed;
        move = move.normalized * currentSpeed;

        move.y = rb.linearVelocity.y; // Garde gravité
        rb.linearVelocity = move;
    }

       // 👇 APPELÉES PAR LES EVENTS D’ANIMATION
    public void StartHit()
    {   
        canAttack = false;
        
        armHit.EnableHit();
    }

    public void StopHit()
    {
        canAttack = true;
        armHit.DisableHit();
    }
}