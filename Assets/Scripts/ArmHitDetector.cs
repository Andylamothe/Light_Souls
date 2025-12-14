    using UnityEngine;

    public class ArmHitDetector : MonoBehaviour
    {
        [Header("Dégâts")]
        private float damage = 25f;
        public float hitCooldown = 0.5f;  // Temps minimum entre deux coups (même si tu frappes plusieurs ennemis)

        private float lastHitTime = 0f;
        private bool canHit = false;

        public void EnableHit()
        {
            canHit = true;
        }

        public void DisableHit()
        {
            canHit = false;
        }

        private void OnTriggerStay(Collider other)
        {
        // Si l'animation d'attaque n'est pas active → rien
        if (Input.GetMouseButtonDown(0))
        { 
            

            // Cooldown global pour éviter les hits toutes les frames
            if (Time.time - lastHitTime < hitCooldown) return;
            
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            
            if (enemy != null)
            {
                
                enemy.TakeDamage(damage);
                lastHitTime = Time.time;   // Un seul coup toutes les "hitCooldown" secondes
                // → On NE met PLUS "canHit = false;" ici
                // → Tu peux taper plusieurs ennemis dans la même animation tant que le cooldown le permet
            }

            // Compatibilité anciens cristaux
            if (other.CompareTag("Target"))
            {
                CrystalHit crystal = other.GetComponent<CrystalHit>();
                crystal?.OnHit();
            }
        }
            
        }

        // Réinitialise pour la prochaine attaque (appelé automatiquement à la fin de l’animation)
        private void OnDisable()
        {
            canHit = false;
        }
    }