using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Grid grid;
    public List<GameObject> interactables;
    public List<GameObject> chefs;
    public Vector3 FindSpotOnGrid(Vector3 spot)
    {
        return spot - grid.gridStart.transform.position; 
    }
    public void PlaceOnGrid(GameObject thing, bool isChef)
    {
        Vector3 spot = FindSpotOnGrid(thing.transform.position);
        int posX = (int) spot.x;
        int posZ = (int)spot.z;
        if (isChef) 
        {
            Grid.tiles[posX, posZ].currentChef = gameObject.GetComponent<Chef>();
        }
        else
        {
            Grid.tiles[posX, posZ].interactable = gameObject.GetComponent<ICanInteract>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
