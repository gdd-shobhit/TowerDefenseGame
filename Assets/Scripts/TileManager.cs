using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject pathTilePrefab;
    public GameObject groundTilePrefab;
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
                //checks to see if the current tile is a path or not
                //if so label the tile as such and create a green tile to represent the path
                //note i and j had to be switched so the array would draw correctly to the screen (just changes position nothing in the array)
                tiles[i, j] = Instantiate(level1[i, j] == '*' ? pathTilePrefab : groundTilePrefab, new Vector3(j * 5, i * -5, 0), new Quaternion()).GetComponent<Tile>();
                tiles[i, j].position = new Vector2Int(i, j);
            }
        }

    }
}
