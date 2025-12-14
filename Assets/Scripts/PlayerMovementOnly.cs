using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementOnly : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Range(0f, 100f)]
    public float defense = 0f;

    [Range(0.1f, 5f)]
    public float speedMultiplier = 1f;

    [Header("Movement")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 6f;

    [Header("References")]
    public Camera playerCamera;
    public ArmHitDetector armHit;
    private Animator anim;
    private Rigidbody rb;
    private PlayerRoll playerRoll;
    private bool canAttack = true;
    private float hitCooldown = 1f;
    private float lastHitTime = 0;
    public HealthBar healthBar;
    [SerializeField] private ResetGame resetGame;
    [SerializeField] private AudioSource swordSwignClip;
    public GameObject defaultSpawn;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerRoll = GetComponent<PlayerRoll>();

        // Si on revient d'une mort, respawn au checkpoint
        // if (Checkpoint.lastCheckpointPosition != Vector3.zero)
        // {
        //     transform.position = Checkpoint.lastCheckpointPosition;
        // }
        transform.position = defaultSpawn.transform.position;
        currentHealth = maxHealth;
        if (playerCamera == null) playerCamera = Camera.main;

        // Initialisation propre de la barre de vie (compatible float + boost)
        if (healthBar != null)
        {
            healthBar.SetMaxHealth((int)maxHealth);
            healthBar.SetHealth((int)currentHealth);
        }

        gameObject.tag = "Player";
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * 5f);

        if (Input.GetKeyDown(KeyCode.LeftControl) && !playerRoll.IsRolling())
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            int rollIndex;
            Vector3 dir;

            if (z > 0) { rollIndex = 0; dir = transform.forward; }
            else if (z < 0) { rollIndex = 1; dir = -transform.forward; }
            else if (x < 0) { rollIndex = 2; dir = -transform.right; }
            else if (x > 0) { rollIndex = 3; dir = transform.right; }
            else { rollIndex = 0; dir = transform.forward; }

            playerRoll.StartRoll(dir, rollIndex);
            return;
        }

        if (playerRoll.IsRolling())
        {
            anim.SetFloat("Speed", 0);
            anim.SetFloat("Strafe", 0);
            anim.SetBool("IsRunning", false);
            return;
        }

        // Animations
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        bool running = Input.GetKey(KeyCode.LeftShift);

        float forwardSpeed = Mathf.Max(0, moveZ);
        float speedValue = forwardSpeed > 0.1f ? (running ? 2f : 1f) : 0f;
        anim.SetFloat("Speed", speedValue);
        anim.SetFloat("Strafe", moveX);
        anim.SetBool("IsRunning", running && moveZ > 0);

        // Attaques
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastHitTime < hitCooldown) return;

            
                lastHitTime = Time.time;
                armHit.EnableHit();
                swordSwignClip.Play();
                anim.SetTrigger("Attack");
            
        }

        // Debug stats
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Health: {currentHealth}/{maxHealth} | Defense: {defense} | Speed Mult: {speedMultiplier}");
        }

        // Test dégâts (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }

    void FixedUpdate()
    {
        if (playerRoll.IsRolling()) return;

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        camForward.y = 0; camRight.y = 0;
        camForward.Normalize(); camRight.Normalize();

        Vector3 move = camForward * Input.GetAxisRaw("Vertical") + camRight * Input.GetAxisRaw("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") > 0 ? runSpeed : walkSpeed;
        currentSpeed *= speedMultiplier;

        move = move.normalized * currentSpeed;
        move.y = rb.linearVelocity.y;
        rb.linearVelocity = move;
    }

    // Animation Events
    public void StartHit()
    {
        canAttack = false;
    }

    public void StopHit()
    {
        canAttack = true;
        armHit.DisableHit();
    }

    // Dégâts reçus (appelé par les ennemis)
    public void TakeDamage(float damage)
    {
        float realDamage = Mathf.Max(0f, damage - defense);
        currentHealth -= realDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        healthBar?.SetHealth(currentHealth);

        Debug.Log($"Dégâts: {realDamage} | Vie: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Joueur mort !");
        resetGame.ResetAllEnemies();
        resetGame.resetPlayerStat();
        transform.position = Checkpoint.lastCheckpointPosition;
        currentHealth = maxHealth;
        healthBar?.SetMaxHealth(maxHealth);
        healthBar?.SetHealth(currentHealth);
        
        // Charger la scène de défaite
        // SceneManager.LoadScene("LosingScene");
    }

    // Boost après kill
    public void Boost(float healthBoost, float defenseBoost, float speedBoost)
    {
        maxHealth += healthBoost;
        currentHealth += healthBoost;
        defense += defenseBoost;
        speedMultiplier += speedBoost;

        healthBar?.SetMaxHealth(maxHealth);
        healthBar?.SetHealth(currentHealth);

        Debug.Log($"Boost ! Vie+{healthBoost} | Def+{defenseBoost} | Vit+{speedBoost}");
    }

    public void setCurrantHealth(float value)
    {
        currentHealth = value;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthBar?.SetHealth(currentHealth);
    }
}