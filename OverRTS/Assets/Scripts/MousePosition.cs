using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class MousePosition : MonoBehaviour
{
    Camera camera;
    public static Vector3 mousePoint;
    private void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        bool pointHit = Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hitInfo);
        mousePoint = hitInfo.point;
    }
}
