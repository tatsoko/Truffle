using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Mapbox.Examples;
//using Mapbox.Examples;

public class ServerTalker : MonoBehaviour
{
    //public static ServerTalker Instance { set; get; }
    private string remote_url = "http://16.170.112.13:8000/api/mushroom/location/";
    private string local_url = "http://localhost:8000/api/mushroom/location/";
    //float latitude = 48.26265f;
    //float longitude = 11.66808f;
    [SerializeField]
    SpawnOnMap spawnOnMap;
    public string request_result;

    void Start()
    {

        StartCoroutine( GetWebData(remote_url) );
       
    }
    

    IEnumerator GetWebData( string address)
    {
        //playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        //var currentPlayerlocation = new GeoCoordinatePortable.GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLon());
        //Debug.Log(playerLocation.GetLocationLat());
        //Debug.Log(playerLocation.GetLocationLon());
        //Debug.Log(currentPlayerlocation.Longitude.ToString());
        LocationStatus playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        
        UnityWebRequest www = UnityWebRequest.Get(address + playerLocation.GetLocationLat().ToString()+playerLocation.GetLocationLon().ToString());

        // UnityWebRequest www = UnityWebRequest.Get(address + latitude.ToString()+"/"+ longitude.ToString());
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            request_result = "request server failed";
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
            // Debug.Log( www.downloadHandler.text );
            request_result = "request server success";
            ProcessServerResponse(www.downloadHandler.text);
            spawnOnMap.SpawnObject();
        }
    }

    void ProcessServerResponse( string rawResponse )
    {
        Debug.Log("test");
        JSONNode node = JSON.Parse( rawResponse );
        for (int i = 0; i < 10; i++)
        {
            string x = node["response"][i]["latitude"]["$numberDecimal"];
            string y = node["response"][i]["longitude"]["$numberDecimal"];
            string test_location = x + "," + y;
           spawnOnMap._locationStrings.Add(test_location);
            
            Debug.Log(test_location);
        }

    }

    private void Update()
    {
        //Instance = this;
    }
}