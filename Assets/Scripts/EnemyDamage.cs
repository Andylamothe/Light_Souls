using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Dégâts infligés au joueur")]
    public float damage = 10f;           
    public float attackDelay = 1.5f;      
    private float nextAttackTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            PlayerMovementOnly player = collision.gameObject.GetComponent<PlayerMovementOnly>();
            if (player != null)
            {
                player.TakeDamage(damage);
                nextAttackTime = Time.time + attackDelay;
            }
        }
    }

    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            PlayerMovementOnly player = collision.gameObject.GetComponent<PlayerMovementOnly>();
            if (player != null)
            {
                player.TakeDamage(damage);
                nextAttackTime = Time.time + attackDelay;
            }
        }
    }
}