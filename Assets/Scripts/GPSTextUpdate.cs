using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Examples;

public class GPSTextUpdate : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI coordinates;
    [SerializeField]
    SpawnOnMap spawnOnMap;
    [SerializeField]
    ServerTalker serverTalker;

    private void Start()
    {
        
    }

    private void Update()
       
    {
        //LocationStatus playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        coordinates.text = "Player Lat:" + GPS.Instance.latitude.ToString() + " Player Log:"
            + GPS.Instance.longitude.ToString() + "\n "+"request status" + serverTalker.request_status+"\n" +"Nearby Mushroom:" 
            + spawnOnMap._locationStrings.Count + "isset Mushroom:" + spawnOnMap.isset;


    }
}
