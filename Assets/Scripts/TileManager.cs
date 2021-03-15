using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    
    public GameObject tilePrefab;
    public Material pathTileMaterial;
    public Material nonPathTileMaterial;
    public Tile[,] tiles;
    public int mapHeight;
    public int mapWidth;


    //level 1 for right now. Stars represent the path
    //and the - represent open sapces
    private char[,] level1 = { 
        {'*','-','-','-','-','*','*','*','-','-'},
        {'*','-','-','-','-','*','-','*','-','-'},
        {'*','-','-','-','-','*','-','*','-','-'},
        {'*','-','-','-','-','*','-','*','-','-'},
        {'*','*','*','*','*','*','-','*','-','-'},
        {'-','-','-','-','-','-','-','*','-','-'},
        {'-','*','*','*','*','*','*','*','-','-'},
        {'-','*','-','-','-','-','-','-','-','-'},
        {'-','*','-','-','-','-','-','-','-','-'},
        {'-','*','-','-','-','-','-','-','-','-'},};

    /// <summary>
    /// Start initializes the path
    /// </summary>
    void Start()
    {
        mapHeight = 10;
        mapWidth = 10;
        tiles = new Tile[mapWidth, mapHeight];

        //nested for loop to go through level1 layout
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                
                //checks to see if the current tile 
                //is a path or not
                if (level1[i,j] == '*')
                {
                    //if so label the tile as such and create a green tile to represent the path
                    //note i and j had to be switched so the array would draw correctly to the screen (just changes position nothing in the array)
                    tiles[i,j] = new Tile(true, false, new Vector3(j * 5, i * -5, 0));
                
                    //create the cube with a green color
                    tilePrefab.GetComponent<MeshRenderer>().material = pathTileMaterial;
                    Instantiate(tilePrefab, tiles[i, j].GetPosition(), new Quaternion());
                }
                else
                {
                    //else do not label the tile a path.
                    //note the same thing as above.
                    tiles[i, j] = new Tile(false, false, new Vector3(j * 5, i * -5, 0));
                
                    //create the cube with a brown color
                    tilePrefab.GetComponent<MeshRenderer>().material = nonPathTileMaterial;
                    Instantiate(tilePrefab, tiles[i, j].GetPosition(), new Quaternion());
                }
            }
        }

    }



    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
    }
}
