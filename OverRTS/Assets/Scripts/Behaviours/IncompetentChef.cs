using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncompetentChef : Chef
{
    public float competency = -15f;

    public override bool Action(float failPercentage)
    {
        float rand = Random.Range(0.0f, 100.0f);
        bool fail = rand + competency <= failPercentage;
        if (fail)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public override void IncreaseCompetency()
    {
        competency++;
    }
}
