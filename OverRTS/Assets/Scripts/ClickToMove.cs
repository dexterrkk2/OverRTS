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
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = Camera.main;
    }
    private void Update()
    {
        bool mousePressed = Input.GetMouseButtonDown(0);
        bool pointHit = MousePosition.mousePoint != null;
        if (mousePressed && pointHit && selected)
        {
            agent.SetDestination(MousePosition.mousePoint);
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