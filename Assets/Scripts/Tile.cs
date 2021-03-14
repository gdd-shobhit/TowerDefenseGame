using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private bool pathTile = false;
    private bool taken = false;
    private Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

    // constructor accepts if its a path tile, if has a tower on top
    //and a position on the screen
    public Tile(bool vPathTile, bool vTaken, Vector3 vPosition)
    {
        pathTile = vPathTile;
        taken = vTaken;
        position = vPosition;
    }

    //grabs if this is a path tile
    public bool getPathTile()
    {
        return pathTile;
    }
    
    //returns if there is a tower on top
    public bool getTaken()
    {
        return taken;
    }

    //can set if there is a tower on top
    public void setTaken(bool vTaken)
    {
        taken = vTaken;
    }

    //grabs the position on the screen.
    public Vector3 getPosition()
    {
        return position;
    }
}
