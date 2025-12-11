using UnityEngine;

public class EnemyHealthBoss : MonoBehaviour
{
    [Header("Vie Ennemi")]
    public float maxHealth = 200f;
    private float currentHealth;

    [Header("Boost donné au joueur à la mort")]
    public float healthBoost = 10f;   
    public float defenseBoost = 2f;   
    public float speedBoost = 0.2f;   

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Boost joueur
        PlayerMovementOnly player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovementOnly>();
        if (player)
        {
            player.Boost(healthBoost, defenseBoost, speedBoost);
        }
        gameObject.SetActive(false);
       
    }
}