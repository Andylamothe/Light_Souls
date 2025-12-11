using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; // Use an array to hold all enemies
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, you could initialize things here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Update logic can be added here if needed
    }

    public void ResetAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            // Activate each enemy
            enemy.SetActive(true);

            // Reset health for each enemy (assuming all enemies have an EnemyHealth script attached)
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.maxHealth = 50;
                // Assuming you want to reset current health as well
            }
        }
    }

    public void resetPlayerStat()
    {
        PlayerMovementOnly playerMovementOnly = player.GetComponent<PlayerMovementOnly>();
        playerMovementOnly.setCurrantHealth(100);
        playerMovementOnly.healthBar.SetMaxHealth(100);
        playerMovementOnly.walkSpeed = 5;
        playerMovementOnly.runSpeed = 12;
    }
}
