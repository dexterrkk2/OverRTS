using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    Camera camera;
    float clickRadius = 2;
    bool selected;
    public float speed;
    public GameObject futurePoint;
    bool isplacingPoint;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = Camera.main;
        agent.speed = speed;
        isplacingPoint = false;
    }
    private void Update()
    {
        bool mousePressed = Input.GetMouseButtonDown(0);
        bool pointHit = MousePosition.mousePoint != null;
        if (mousePressed && pointHit && selected)
        {
            NavMeshPath path  = new NavMeshPath();
            agent.SetDestination(MousePosition.mousePoint);
            float distance = (transform.position - MousePosition.mousePoint).magnitude;
            Grid.RemoveFromGrid(gameObject, true);
            selected = false;
        }
        if (pointHit && mousePressed)
        {
            bool inRange = (MousePosition.mousePoint - transform.position).magnitude < clickRadius;
            if (inRange)
            {
                selected = true;
            }
        }
        bool canPlacePoint = agent.pathPending == false && isplacingPoint == false && agent.hasPath == true;
        if (canPlacePoint)
        {
            Debug.Log(agent.remainingDistance);
            float timeToPoint = agent.speed * agent.remainingDistance;
            isplacingPoint = true;
            Invoke("PlaceSelfOnGraph", timeToPoint);
        }
    }
    public void PlaceSelfOnGraph()
    {
        Debug.Log("placingPoint");
        bool isOnTarget = (transform.position - MousePosition.mousePoint).magnitude <= Grid.tileSize;
        Debug.Log("is on target: " + isOnTarget);
        if (isOnTarget)
        {
            Debug.Log("isPlaced");
            Grid.PlaceOnGrid(gameObject, true);
        }
        isplacingPoint = false;
    }
}
