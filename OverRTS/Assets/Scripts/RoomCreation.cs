using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
public class RoomCreation : MonoBehaviour
{
    public int width;
    public int height;
    public int randomPlacement;
    public GameObject startingRoom;
    public List<GameObject> roomPrefabs;
    public List<Location> locations;
    public GameObject pathwayPrefab;
    public void CreateRoom()
    {
        locations = new List<Location>();
        Room room = startingRoom.GetComponent<Room>();
        Location location2 = new Location();
        location2.prefab = room;
        location2.origin = Vector3.zero;
        StartAt(location2);
        int difference = 0;
        Instantiate(startingRoom, location2.origin, Quaternion.identity);
        for (int incrementZ =0; incrementZ<height; incrementZ+= room.height + difference)
        {
            for (int incrementX =0 ; incrementX < width; incrementX+= room.width + difference)
            {
                difference= Random.Range(1, randomPlacement);
                int rand = Random.Range(0, roomPrefabs.Count);
                room = roomPrefabs[rand].GetComponent<Room>();
                Location location = new Location();
                //Debug.Log(room);
                location.prefab = room;
                Vector3 placespot = new Vector3(incrementX + difference, 0, incrementZ +difference);
                location.origin = placespot;
                //Debug.Log(location.origin);
                location = MakeConnection(location);
                //Debug.Log(location);
                //Debug.Log(incrementX);
                if (location != null)
                {
                    //Debug.Log("print Room");
                    Instantiate(roomPrefabs[rand], location.origin, Quaternion.identity);
                    //incrementX += difference + room.width;
                    //incrementZ = Mathf.Max(incrementZ, room.height);
                    buildConnection(location);
                    locations.Add(location);
                }
            }
            //incrementZ += room.height;
        }
    }
    public void StartAt(Location location)
    {
        //Debug.Log(location);
        locations.Add(location);
    }
    public Location MakeConnection(Location location)
    {
        List<Vector3> exits = location.prefab.GetExits();
        //Shuffle
        for(int i=0; i< exits.Count; i++)
        {
            //Debug.Log("Exits");
            int rand = Random.Range(0, exits.Count);
            Vector3 previousExit = exits[i];
            exits[i] = exits[rand];
            exits[rand] = previousExit;
        }
        foreach(Vector3 exit in exits)
        {
            int rand = Random.Range(0, roomPrefabs.Count);
            GameObject prefab = roomPrefabs[rand];
            Vector3 origin = location.origin;
            Location candidate = new Location();
            candidate.origin = origin;
            candidate.prefab = prefab.GetComponent<Room>();
            if (CollisionDetection(candidate))
            {
                Vector3 passageFrom = exit - origin;
                candidate.passageFrom = passageFrom;               
                return candidate;
            }
        }
        return null;
    }
    public bool CollisionDetection(Location location)
    {
        foreach (Location locationCheck in locations)
        {
            int rangeX = Mathf.Max(location.prefab.width, locationCheck.prefab.width);
            int rangeZ = Mathf.Max(location.prefab.height, locationCheck.prefab.height);
            Vector3 difference = location.origin - locationCheck.origin;
            bool horizontalCheck = difference.x >= difference.z;
            if (horizontalCheck)
            {
                //Debug.Log(difference.magnitude);
                //Debug.Log(rangeX);
                if (difference.magnitude <= rangeX)
                {
                    return false;
                }
            }
            else
            {
                //Debug.Log(difference.magnitude);
                //Debug.Log(rangeZ);
                if (difference.magnitude <= rangeZ)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void buildConnection(Location location)
    {
        int rand = Random.Range(0, locations.Count);
        Location connection = locations[rand];
        NavMeshLink link = gameObject.AddComponent<NavMeshLink>();
        List<Vector3> Exits = location.prefab.GetExits();
        List<Vector3> candidateExits = connection.prefab.GetExits();
        int randomExit = Random.Range(0, candidateExits.Count);
        link.startPoint = location.origin;
        link.endPoint = connection.origin; 
        link.width = 1;
    }
}
