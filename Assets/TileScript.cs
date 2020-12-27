using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public Rigidbody rgbody;
    public TileSpawner tileSpawner;
    GameObject spawner;
    public int pieceID;
    static bool canHold = true;
    public bool objRight;
    public bool objLeft;
    public int[] pos1 = new int[4];
    public int[] pos2 = new int[4];
    public int[] pos3 = new int[4];
    public int[] pos4 = new int[4];
    int rotationCounter = 0;
    public float timer = 0;
    public GameObject[] blocks = new GameObject[1];
    int[][] positions;
    public GameObject lineCheck;
    public bool leftTimer = false;
    public bool rightTimer = false;
    float timerR = 0;
    float timerL = 0;
    public GameObject Score;
    GameObject holdController;
    private void Start()
    {
        positions = new int[][] { pos1, pos2, pos3, pos4 };
        lineCheck = GameObject.Find("LineChecker");
        Score = GameObject.Find("Score");
        holdController = GameObject.Find("HoldController");
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Landed") {
            timer += 1 * Time.deltaTime;
        }
        if (timer >0.5) {
            spawner = GameObject.Find("Spawner");
            tileSpawner = spawner.GetComponent<TileSpawner>();
            rgbody.isKinematic = true;
            gameObject.tag = "Landed";
            for (int j = 0; j < blocks.Length; j++)
            {
                blocks[j].tag = "Landed";
            }
            tileSpawner.spawnTile = true;
            for (int i = 0; i<blocks.Length; i++) {
                if (!blocks[i].gameObject.GetComponent<BoxCollider>().isTrigger) {
                    blocks[i].gameObject.transform.parent = null;
                }
                else
                {
                    Destroy(blocks[i].gameObject);
                }
            }
            lineCheck.GetComponent<LineChecker>().checkline = true;
            gameObject.GetComponent<AudioSource>().Play();
            canHold = true;
            this.enabled = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        timer = 0;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow)) {
            rgbody.velocity = new Vector3(0, -7f + (-0.5f * Score.GetComponent<ScoreController>().level), 0);
        }
        else
        {
            rgbody.velocity = new Vector3(0, -2f+(-0.5f*Score.GetComponent<ScoreController>().level), 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && canHold)
        {
            Hold();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftTimer = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftTimer = false;
            timerL = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightTimer = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightTimer = false;
            timerR = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && checkForMovement(-1))
        {
            rgbody.MovePosition(transform.position - (Vector3.right));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && checkForMovement(1))
        {
            rgbody.MovePosition(transform.position + (Vector3.right));
        }
        if (rightTimer && !leftTimer)
        {
            timerR += Time.deltaTime;
        }
        if (leftTimer && !rightTimer)
        {
            timerL += Time.deltaTime;
        }
        if (timerR >= 0.12f)
        {
            if (checkForMovement(1))
            {
                rgbody.MovePosition(transform.position + (Vector3.right));
            }
            timerR = 0;
        }
        if (timerL >= 0.12f)
        {
            if (checkForMovement(-1))
            {
                rgbody.MovePosition(transform.position - (Vector3.right));
            }
            timerL = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FastFall();
        }
    }
    private void Rotate()
    {
        //Rotates the tetris peice, by toggling the correct gameobjects
        //returns the position that the tile is in
        if (rotationCounter<3 && checkForFreeBlock(positions[rotationCounter + 1]))
        {
            ToggleBlock(positions[rotationCounter]);
            rotationCounter++;
            ToggleBlock(positions[rotationCounter]);
        }
        else if(checkForFreeBlock(positions[0]))
        {
            ToggleBlock(positions[rotationCounter]);
            rotationCounter = 0;
            ToggleBlock(positions[rotationCounter]);
        }
    }
    private bool checkForFreeBlock(int[] blockNumber)
    {
        //Checks to see if the way is clear to rotate
        bool isClear = true;
        for (int i = 0; i < blockNumber.Length; i++)
        {
            if (blocks[blockNumber[i]].GetComponent<BlockScript>().collided)
            {
                isClear = false;
            }
        }
        return isClear;
    }
    private void ToggleBlock(int[] blockNumber)
    {
        //toggles the blocks to be shown
        for (int i = 0; i<blockNumber.Length; i++) {
            if (blocks[blockNumber[i]].GetComponent<BoxCollider>().isTrigger)
            {
                blocks[blockNumber[i]].GetComponent<BoxCollider>().isTrigger = false;
                blocks[blockNumber[i]].GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                blocks[blockNumber[i]].GetComponent<BoxCollider>().isTrigger = true;
                blocks[blockNumber[i]].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    private bool checkForMovement(int x)
    {
        bool safeToMove;

        //Casts a shadow the the tetris piece to the right or left one block -1 for left 1 for right
        bool isClear = true;
        for (int i = 0; i < 4; i++)
        {
            if (Physics.CheckBox(blocks[positions[rotationCounter][i]].transform.position + (x * (Vector3.right)), new Vector3(0.2f, 0.2f, 0.2f), transform.rotation)) 
            {
                bool foundName = false;
                for (int j = 0; j < blocks.Length; j++)
                {
                    if (Physics.OverlapBox(blocks[positions[rotationCounter][i]].transform.position + (x * (Vector3.right)), new Vector3(0.2f, 0.2f, 0.2f), transform.rotation)[0].tag == blocks[j].tag)
                    {
                        foundName = true;
                    }
                }
                if (!foundName)
                {
                    isClear = false;
                }
            }
        }
        if (isClear)
        {
            safeToMove = true;
        }
        else
        {
            safeToMove = false;
        }
        return safeToMove;
    }
    private void FastFall()
    {
        bool breakloops = false;
        for (int j =0; j<23; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Physics.CheckBox(new Vector3(Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.x), Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.y) - j, Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.z)), new Vector3(0.4f, 0.4f, 0.4f), transform.rotation))
                {
                    Collider[] m = Physics.OverlapBox(new Vector3(Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.x), Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.y) - j, Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.z)), new Vector3(0.4f, 0.4f, 0.4f), transform.rotation, ~0, QueryTriggerInteraction.Ignore);
                    for (int l = 0; l < m.Length; l++)
                    {
                        if (m[l].gameObject.tag == "Landed" || m[l].gameObject.tag == "Floor") {
                            transform.position = new Vector3(transform.position.x, Mathf.Round(blocks[positions[rotationCounter][i]].transform.position.y) - (j - 1), transform.position.z);
                            breakloops = true;
                            break;
                        }
                    }
                }
                if (breakloops)
                {
                    break;
                }
            }
            if (breakloops)
            {
                break;
            }
        }
    }
    void Hold()
    {
        spawner = GameObject.Find("Spawner");
        tileSpawner = spawner.GetComponent<TileSpawner>();
        if (tileSpawner.tileToSpawn != 10) {
            tileSpawner.tileToSpawn = holdController.GetComponent<NextController>().next;
            tileSpawner.spawnSpecificTile = true;
        }
        else
        {
            tileSpawner.spawnTile = true;
            tileSpawner.tileToSpawn = pieceID;
        }
        holdController.GetComponent<NextController>().next = pieceID;
        canHold = false;
        Destroy(gameObject);
    }
}
