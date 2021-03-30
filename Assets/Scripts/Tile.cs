using UnityEngine;

/// <summary>
/// Tile is a class that holds whether it is a path tile, if there is a 
/// tower on it and the position,
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool pathTile = false;
    public TowerInstance occupyingTower = null;
    public Vector2Int position;

    /// <summary>
    /// Grabs if this is a path tile
    /// </summary>
    public bool GetPathTile()
    {
        return pathTile;
    }

    /// <summary>
    /// Returns if there is a tower on top
    /// </summary>
    public bool IsTaken()
    {
        return occupyingTower != null;
    }

    /// <summary>
    /// Returns the position of the tile in the tile array
    /// </summary>
    public Vector2Int GetPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.y / 5), Mathf.RoundToInt(transform.position.x / -5));
    }

    /// <summary>
    /// Tells the GameManager when this tile is clicked
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("clicked");
        GameManager.instance.TileClicked(position);
    }
}
