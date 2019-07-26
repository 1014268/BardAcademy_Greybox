using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public Transform gateTarget;
    public GameObject playerCharacter;

    public void OnTriggerEnter(Collider other)
    {
        playerCharacter.transform.position = gateTarget.transform.position;
        Debug.Log(gateTarget.transform.position);
    }
}    
