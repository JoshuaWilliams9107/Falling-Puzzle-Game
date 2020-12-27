using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Light>().intensity>0) {
            gameObject.GetComponent<Light>().intensity -= 0.1f;
        }
	}
}
