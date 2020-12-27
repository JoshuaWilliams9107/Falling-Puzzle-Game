using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {
    public GameObject tPiece;
    public GameObject gSPiece;
    public GameObject rSPiece;
    public GameObject sPiece;
    public GameObject lPiece;
    public GameObject bLPiece;
    public GameObject oLPiece;
    GameObject[] array = new GameObject[7];
    public bool spawnTile = false;
    public bool spawnSpecificTile = false;
    int next = 0;
    public GameObject nextController;
    public int tileToSpawn = 10;
    private void Start()
    {
        next = Random.Range(0, 7);
        array[0] = tPiece;
        array[1] = gSPiece;
        array[2] = rSPiece;
        array[3] = sPiece;
        array[4] = lPiece;
        array[5] = bLPiece;
        array[6] = oLPiece;
        spawnNewTile();

    }
    private void Update()
    {
        if (spawnTile)
        {
            spawnNewTile();
            spawnTile = false;
        }
        if (spawnSpecificTile)
        {
            spawnSpecificTiles();
            spawnSpecificTile = false;
        }
    }
    public void spawnNewTile()
    {
        Instantiate(array[next], transform.position, Quaternion.identity);
        next = Random.Range(0, 7);
        nextController.GetComponent<NextController>().next = next;
    }
    public void spawnSpecificTiles()
    {
        Instantiate(array[tileToSpawn], transform.position, Quaternion.identity);
    }
}
