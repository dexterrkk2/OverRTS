using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using TMPro;
public interface ICanInteract
{
    void RunJob(IChef chef);
}
public class Interactable : MonoBehaviour, ICanInteract
{
    public TextMeshProUGUI successText;
    public float failPercentage;
    public float jobTime;
    public float coolDown;
    float timer;
    public float iterrationsToIncrease;
    float iterrations;
    void Awake()
    {
        Debug.Log(gameObject);
        GameManager.interactables.Add(gameObject);
    }
    public void RunJob(IChef chef)
    {
        //Debug.Log("Run Job");
        //Debug.Log("TImer:" + timer);
        if (timer <= 0)
        {
            bool sucess = chef.Action(failPercentage);
            if (sucess)
            {
                successText.text = "Success";
                if (iterrations > iterrationsToIncrease)
                {
                    chef.IncreaseCompetency();
                    iterrations = 0;
                }
                GameManager.IncreaseScore();
            }
            else
            {
                successText.text = "Fail";
                if (iterrations > iterrationsToIncrease)
                {
                    failPercentage++;
                    iterrations = 0;
                }
            }
            iterrations++;
            timer = coolDown;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = coolDown;
        iterrations = 0;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        /*bool mousePressed = Input.GetMouseButtonDown(0);
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
        }*/
    }
}
