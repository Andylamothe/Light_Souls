using UnityEngine;

public class HandleAttackAnimationEnemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AiAgent aiAgent;

    private AnimatorStateInfo stateInfo;

    private float attackCooldown = 1.5f;     // Time between attacks
    private float nextAttackTime = 0f;       // Timestamp when next attack is allowed

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isInAttack = stateInfo.IsName("Attack1");
        bool attackFinished = stateInfo.normalizedTime >= 1f;

        // Enemy is attacking while animation is in progress
        aiAgent.setIsAttacking(isInAttack && !attackFinished);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("playerDistanceHit"))
            return;

        // Cooldown not finished? → no attack
        if (Time.time < nextAttackTime)
            return;

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isInAttack = stateInfo.IsName("Attack1");
        bool isTransitioning = animator.IsInTransition(0);

        // Avoid double triggers (do not trigger during transition)
        if (!isInAttack && !isTransitioning)
        {
            animator.SetTrigger("attack");
            nextAttackTime = Time.time + attackCooldown; // set 1.5s cooldown
        }
    }
}
