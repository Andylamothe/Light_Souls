
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class AiAgentWithAnimator : MonoBehaviour
{
    public Transform Target;
    public Transform InitialPosition;
    private NavMeshAgent agent;
    public TargetColisionPlayer TargetColisionPlayer;   
    public Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {   
        float speed = agent.velocity.magnitude; 
        Debug.Log(TargetColisionPlayer.getPlayerIsInCOntact());
        if(TargetColisionPlayer.getPlayerIsInCOntact()){
            agent.SetDestination(Target.position);
              
                

        } 
        else{
             agent.SetDestination(InitialPosition.position);
        }
       
       animator.SetFloat("speed", speed);
    }
}
 