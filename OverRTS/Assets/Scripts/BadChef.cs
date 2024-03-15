using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadChef : MonoBehaviour, IChef
{
    public IncompetentChef chef = new IncompetentChef();

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Action(float failpercentage)
    {
        return chef.Action(failpercentage);
    }

    public void IncreaseCompetency()
    {
        chef.IncreaseCompetency();
        //Debug.Log("Compotency: " + chef.competency);
    }
}
