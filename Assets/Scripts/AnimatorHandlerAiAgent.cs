
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class AiAgentWithAnimator : MonoBehaviour
{

    private NavMeshAgent agent;
    public Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {   
        float speed = agent.velocity.magnitude; 
        animator.SetFloat("speed", speed);
    }
}
 