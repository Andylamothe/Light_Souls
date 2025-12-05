using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public Transform Target;
    public Transform InitialPosition;
    private NavMeshAgent agent;
    public TargetColisionPlayer TargetColisionPlayer;

    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (!isAttacking)
        {
            if (TargetColisionPlayer.getPlayerIsInCOntact())
            {
                agent.SetDestination(Target.position);
            }
            else
            {
                agent.SetDestination(InitialPosition.position);
            }
        }
    }

    public void setIsAttacking(bool attacking)
    {
        isAttacking = attacking;

        if (attacking)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
    }
}
