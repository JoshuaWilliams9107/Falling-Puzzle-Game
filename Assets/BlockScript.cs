using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    public bool collided = false;
    private void OnTriggerStay(Collider other)
    {
        collided = true;
    }
    private void Start()
    {

    }
    private void OnTriggerExit(Collider other)
    {
        collided = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
}
