using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
//using Mapbox.Examples;

public class ServerTalker : MonoBehaviour
{
    private string remote_url = "http://16.170.112.13:8000/api/mushroom/location/";
    private string local_url = "http://localhost:8000/api/mushroom/location/";
    public double latitude = 48.15945;
    public double longitude = 11.56434;
    //LocationStatus playerLocation;
    //[SerializeField]
    //SpawnOnMap spawnOnMap;
    // Start is called before the first frame update

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
        //UnityWebRequest www = UnityWebRequest.Get(address + GPS.Instance.latitude.ToString()+GPS.Instance.longitude.ToString());
        UnityWebRequest www = UnityWebRequest.Get(address + latitude.ToString()+"/"+ longitude.ToString());
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
           // Debug.Log( www.downloadHandler.text );

            ProcessServerResponse(www.downloadHandler.text);
            //spawnOnMap.SpawnObject();
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
           //spawnOnMap._locationStrings.Add(test_location);
            
            Debug.Log(test_location);
        }

        Vector2 position = new Vector2(node["response"][0]["latitude"]["$numberDecimal"].AsFloat, node["response"][0]["longitude"]["$numberDecimal"].AsFloat);

        GetComponent<MushroomPosition>().SetPosition(position);
    }

    
}