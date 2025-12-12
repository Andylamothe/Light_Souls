using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vie Ennemi")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("Boost donné au joueur à la mort")]
    public float healthBoost = 5f;   
    public float defenseBoost =0.5f;   
    public float speedBoost = 0.05f;   

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
        Debug.Log(currentHealth);
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
        Debug.Log(gameObject.name);
        if (gameObject.name == "bossFinal")
        {
            SceneManager.LoadScene("VictoryScene");
        }
        gameObject.SetActive(false);
    }
}