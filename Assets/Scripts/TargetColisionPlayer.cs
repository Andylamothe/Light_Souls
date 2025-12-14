using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetColisionPlayer : MonoBehaviour
{

    private bool playerIsInContact = false;
    private GameObject surface;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnTriggerStay(Collider other)
{
    
    

    if (other.CompareTag("Player"))
    {
       
        playerIsInContact = true;
    }
}
 private void OnTriggerExit(Collider other)
{
   

    if (other.CompareTag("Player"))
    {
        playerIsInContact = false;
    }
}

public bool getPlayerIsInCOntact(){
    return playerIsInContact;
}
}
