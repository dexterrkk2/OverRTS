using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour
{
    public int maxX;
    public int maxZ;
    public int tileSizeSet;
    public static int tileSize;
    public static Tile[,] tiles;
    public GameObject gridStartObject;
    public static GameObject gridStart;
    public GameObject gridEnd;
    // Start is called before the first frame update
    public void Spawn()
    {
        tileSize = tileSizeSet;
        gridStart = gridStartObject;
        maxX = (int)(gridEnd.transform.position.x - gridStart.transform.position.x) ;
        maxZ = (int)(gridEnd.transform.position.z - gridStart.transform.position.z);
        int maxTileX = maxX / tileSize;
        int maxTileZ = maxZ / tileSize;
        tiles = new Tile[maxTileX, maxTileZ];
        for(int i =0; i < maxTileX; i++)
        {
            for (int j = 0; j < maxTileZ; j++)
            {
                tiles[i, j] = new Tile();
            }
        }

    }
    public static Vector3 FindSpotOnGrid(Vector3 spot)
    {
        return (spot - gridStart.transform.position) / tileSize;
    }
    public static void PlaceOnGrid(GameObject thing, bool isChef)
    {
        Vector3 spot = FindSpotOnGrid(thing.transform.position);
        int posX = (int)spot.x;
        int posZ = (int)spot.z;
        if (isChef)
        {
            tiles[posX, posZ].currentChef = thing.GetComponent<IChef>();
        }
        else
        {
            tiles[posX, posZ].interactable = thing.GetComponent<ICanInteract>();
        }
    }
    public static void RemoveFromGrid(GameObject thing, bool isChef)
    {
        Vector3 spot = FindSpotOnGrid(thing.transform.position);
        int posX = (int)spot.x;
        int posZ = (int)spot.z;
        if (isChef)
        {
            tiles[posX, posZ].currentChef = null;
        }
        else
        {
            tiles[posX, posZ].interactable = null;
        }
    }
}
