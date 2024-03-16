using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour
{
    public int maxX;
    public int maxZ;
    public int tileSize;
    public static Tile[,] tiles;
    public GameObject gridStart;
    public GameObject gridEnd;
    private void Start()
    {
        Spawn();
    }
    // Start is called before the first frame update
    public void Spawn()
    {
        maxX = (int)(gridEnd.transform.position.x - gridStart.transform.position.x) ;
        maxZ = (int)(gridEnd.transform.position.z - gridStart.transform.position.z);
        tiles = new Tile[maxX/tileSize, maxZ/tileSize];
    }
}
