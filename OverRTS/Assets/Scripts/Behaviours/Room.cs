using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room: MonoBehaviour
{
    public int width;
    public int height;
    public List<Transform> Exits;
    public List<Vector3> GetExits()
    {
        List<Vector3> exits = new List<Vector3>();
        for (int i=0; i<Exits.Count; i++)
        {
            exits.Add(Exits[i].transform.position);
        }
        return exits;
    }
}