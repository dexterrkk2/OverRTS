using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChef
{
    public bool Action(float failPercentage);
    public void IncreaseCompetency();
}
public abstract class Chef: IChef
{
    public abstract bool Action(float failPercentage);

    public abstract void IncreaseCompetency();
}
