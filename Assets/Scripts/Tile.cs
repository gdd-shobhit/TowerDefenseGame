using UnityEngine;

/// <summary>
/// Tile is a class that holds whether it is a path tile, if there is a 
/// tower on it and the position,
/// </summary>
public class Tile
{
    private bool pathTile = false;
    private bool taken = false;
    private Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

    /// <summary>
    /// constructor accepts if its a path tile, if has a tower on top
    /// and a position on the screen
    /// </summary>
    /// <param name="vPathTile"></param>
    /// <param name="vTaken"></param>
    /// <param name="vPosition"></param>
    public Tile(bool vPathTile, bool vTaken, Vector3 vPosition)
    {
        pathTile = vPathTile;
        taken = vTaken;
        position = vPosition;
    }

    /// <summary>
    /// grabs if this is a path tile
    /// </summary>
    /// <returns></returns>
    public bool GetPathTile()
    {
        return pathTile;
    }

    /// <summary>
    /// returns if there is a tower on top
    /// </summary>
    /// <returns></returns>
    public bool GetTaken()
    {
        return taken;
    }

    /// <summary>
    /// can set if there is a tower on top
    /// </summary>
    /// <param name="vTaken"></param>
    public void SetTaken(bool vTaken)
    {
        taken = vTaken;
    }

    /// <summary>
    /// grabs the position on the screen.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosition()
    {
        return position;
    }
}
