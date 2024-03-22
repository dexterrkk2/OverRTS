using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Grid grid;
    public List<GameObject> interactables;
    public List<GameObject> chefs;
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        bool isChef;
        grid.Spawn();
        for (int i=0; i<interactables.Count; i++)
        {
            GameObject thing = interactables[i];
            isChef = false;
            Grid.PlaceOnGrid(thing, isChef);
        }
        for (int i = 0; i < chefs.Count; i++)
        {
            GameObject thing = chefs[i];
            isChef = true;
            Grid.PlaceOnGrid(thing, isChef);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void IncreaseScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}
