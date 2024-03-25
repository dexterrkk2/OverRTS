using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Grid grid;
    public static List<GameObject> interactables;
    public int chefCount;
    public List<GameObject> chefPrefabs;
    public RoomCreation roomCreation;
    public NavMeshSurface surface;
    public TextMeshProUGUI scoreText;
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        bool isChef;
        interactables = new List<GameObject>();
        roomCreation.CreateRoom();
        grid.gridEnd.transform.position = new Vector3(roomCreation.width+10,0f, roomCreation.height+10);
        surface.BuildNavMesh();
        grid.Spawn();
        for (int i = 0; i < interactables.Count; i++)
        {
            GameObject thing = interactables[i];
            isChef = false;
            Grid.PlaceOnGrid(thing, isChef);
        }
        for (int i = 0; i < chefCount; i++)
        {
            int rand = Random.Range(0, chefPrefabs.Count);
            GameObject thing = Instantiate(chefPrefabs[rand]);
            int randLocation = Random.Range(0,roomCreation.locations.Count);
            thing.transform.position = roomCreation.locations[randLocation].origin;
            isChef = true;
            Grid.PlaceOnGrid(thing, isChef);
        }
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
    public static void IncreaseScore()
    {
        score++;
    }
}
