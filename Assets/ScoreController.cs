using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public float lines=0;
    public float level=0;
    float prevlines=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame 
	void Update () {
        if (prevlines != lines)
        {
            prevlines = lines;
            level = Mathf.Floor(lines/10);
            gameObject.GetComponent<TextMesh>().text = "LEVEL: " + level + "\nLINES:  " + lines;
        }
	}
}
