using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public Transform gateTarget;
    public GameObject playerCharacter;

    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider playerCharacter)
    {
        playerCharacter.transform.position = gateTarget.transform.position;
        Debug.Log("Gate target = " + gateTarget.transform.position);
        Debug.Log("Player Position = " + playerCharacter.transform.position);
    }
}    
