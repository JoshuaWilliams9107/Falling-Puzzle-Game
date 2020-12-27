using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextController : MonoBehaviour {
    public GameObject[] display;
    public int[] linePiece = new int[4];
    public int[] TPiece = new int[4];
    public int[] BLPiece = new int[4];
    public int[] OLPiece = new int[4];
    public int[] GSPiece = new int[4];
    public int[] RSPiece = new int[4];
    public int[] Square = new int[4];
    public Sprite[] colors;
    public int[][] pieces;
    public int next = 0;
    int prevnext = 0;
    public bool willSwitch;
    public bool hasSwitched;
	// Use this for initialization
	void Start () {
        pieces = new int[][] {TPiece,RSPiece, GSPiece, Square,linePiece,BLPiece,OLPiece};
        prevnext = next;
        if (!willSwitch) {
            ToggleBlock(pieces[prevnext]);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (willSwitch && hasSwitched && prevnext != next)
        {
            prevnext = next;
            setColor();
            ToggleBlock(pieces[next]);
            hasSwitched = false;
        }
        else
        if (prevnext != next)
        {
            ToggleBlock(pieces[prevnext]);
            prevnext = next;
            setColor();
            ToggleBlock(pieces[next]);
        }
	}
    void setColor()
    {
        for (int i = 0; i<display.Length; i++)
        {
            display[i].GetComponent<SpriteRenderer>().sprite = colors[next];
        }
    }
    private void ToggleBlock(int[] blockNumber)
    {
        //toggles the blocks to be shown
        for (int i = 0; i < blockNumber.Length; i++)
        {
            if (display[blockNumber[i]].GetComponent<SpriteRenderer>().enabled)
            {
                display[blockNumber[i]].GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                display[blockNumber[i]].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
