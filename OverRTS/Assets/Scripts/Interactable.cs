using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
public interface ICanInteract
{
    void RunJob(IChef chef);
}
public class Interactable : MonoBehaviour, ICanInteract
{
    public float failPercentage;
    public IChef IChef;
    public GameObject chef;
    bool selected;
    float clickRadius = 2;
    public float jobTime;
    public float coolDown;
    float timer;
    public float iterrationsToIncrease;
    float iterrations;
    public void RunJob(IChef chef)
    {
        bool sucess = IChef.Action(failPercentage);
        if (sucess)
        {
            Debug.Log("success");
            if (iterrations > iterrationsToIncrease)
            {
                chef.IncreaseCompetency();
                iterrations = 0;
            }
        }
        else
        {
            Debug.Log("Fail");
            if (iterrations > iterrationsToIncrease)
            {
                failPercentage++;
                iterrations = 0;
            }
        }
        iterrations++;
    }

    // Start is called before the first frame update
    void Start()
    {
        IChef = chef.GetComponent<IChef>();
        timer = coolDown;
        iterrations = 0;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        bool mousePressed = Input.GetMouseButtonDown(0);
        bool pointHit = MousePosition.mousePoint != null;
        bool timerEnded = timer <= 0;
        if (mousePressed && pointHit && selected && timerEnded)
        {
            RunJob(IChef);
            selected = false;
            timer = coolDown;
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
