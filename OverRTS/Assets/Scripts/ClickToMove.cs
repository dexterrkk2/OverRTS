using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    float clickRadius = 2;
    bool selected;
    public float speed;
    bool isplacingPoint;
    public float gridOffset;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        isplacingPoint = false;
    }
    private void Update()
    {
        bool mousePressed = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);
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
        bool canPlacePoint = (agent.pathPending == false && isplacingPoint == false && agent.hasPath && agent.remainingDistance != 0);
        if (canPlacePoint)
        {
            //Debug.Log(agent.remainingDistance);
            if (agent.remainingDistance < Mathf.Infinity)
            {
                float timeToPoint = agent.remainingDistance / agent.speed;
                //Debug.Log(timeToPoint);
                isplacingPoint = true;
                Invoke("PlaceSelfOnGraph", timeToPoint);
            }
        }
        if (rightClick)
        {
            selected = false;
        }
    }
    public void PlaceSelfOnGraph()
    {
        //Debug.Log("placingPoint");
        float distance = (transform.position - agent.destination).magnitude;
        bool isOnTarget = distance-gridOffset <= Grid.tileSize;
        //Debug.Log(distance);
        //Debug.Log(Grid.tileSize);
        //Debug.Log("is on target: " + isOnTarget);
        if (isOnTarget)
        {
            //Debug.Log("isPlaced");
            Grid.PlaceOnGrid(gameObject, true);
            Grid.CheckTile(gameObject.transform.position);
        }
        isplacingPoint = false;
    }
}
