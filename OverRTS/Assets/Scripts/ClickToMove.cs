using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    Camera camera;
    float clickRadius = 2;
    bool selected;
    public float speed;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = Camera.main;
        agent.speed = speed;
    }
    private void Update()
    {
        bool mousePressed = Input.GetMouseButtonDown(0);
        bool pointHit = MousePosition.mousePoint != null;
        if (mousePressed && pointHit && selected)
        {
            agent.SetDestination(MousePosition.mousePoint);
            float timeToPoint = agent.speed * agent.remainingDistance; 
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
        
    }
}
