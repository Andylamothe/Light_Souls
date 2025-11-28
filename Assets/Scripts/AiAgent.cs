
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class AiAgent : MonoBehaviour
{
    public Transform Target;
    public Transform InitialPosition;
    private NavMeshAgent agent;
    public TargetColisionPlayer TargetColisionPlayer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {   
        Debug.Log(TargetColisionPlayer.getPlayerIsInCOntact());
        if(TargetColisionPlayer.getPlayerIsInCOntact()){
            agent.SetDestination(Target.position);
        } 
        else{
             agent.SetDestination(InitialPosition.position);
        }
       
    }
}
 