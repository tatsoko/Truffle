using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Examples;

public class GPSTextUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coordinates;
    [SerializeField]
    SpawnOnMap spawnOnMap;
    ServerTalker serverTalker;

    private void Update()
       
    {
        LocationStatus playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        coordinates.text = "Player Lat:" + playerLocation.GetLocationLat().ToString() + " Player Log:" + playerLocation.GetLocationLon().ToString() + "\n "+"request status" + serverTalker.request_result+"\n" +"Nearby Mushroom:" + spawnOnMap._locationStrings.Count;


    }
}
