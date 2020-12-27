using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDetector : MonoBehaviour {
    public GameObject parent;
    public Vector3 rotationPosition0;
    public Vector3 rotationPosition90;
    public Vector3 rotationPosition180;
    public Vector3 rotationPosition270;
    public bool isRight;
    private void Update()
    {
        if (parent.transform.eulerAngles.z >= 0 && parent.transform.eulerAngles.z <= 0.2f)
            transform.localPosition = rotationPosition0;
        if (parent.transform.eulerAngles.z == 90)
            transform.localPosition = rotationPosition90;
        if (parent.transform.eulerAngles.z == 180)
            transform.localPosition = rotationPosition180;
        if (parent.transform.eulerAngles.z == 270)
            transform.localPosition = rotationPosition270;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isRight)
            parent.GetComponent<TileScript>().objRight = true;
        else
            parent.GetComponent<TileScript>().objLeft = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (isRight)
            parent.GetComponent<TileScript>().objRight = false;
        else
            parent.GetComponent<TileScript>().objLeft = false;
    }
}
