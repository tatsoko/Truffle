using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Mapbox.Examples;
using System.Net;
//using Mapbox.Examples;

public class ServerTalker : MonoBehaviour
{
    public static ServerTalker Instance { set; get; }
    private string remote_url = "http://16.170.112.13:8000/api/mushroom/location/";
    float latitude = 48.26265f;
    float longitude = 11.66808f;
    [SerializeField]
    SpawnOnMap spawnOnMap;
    public string request_status;
    bool hasSpawned;


    IEnumerator GetWebData(string address)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + GPS.Instance.latitude.ToString() + "/" + GPS.Instance.longitude.ToString());
        // UnityWebRequest www = UnityWebRequest.Get(address + latitude.ToString()+"/"+ longitude.ToString());
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            request_status = "request to " + address + "\n" + GPS.Instance.latitude.ToString() + "/" + GPS.Instance.longitude.ToString() + "\n" + "failed";
            // request_status = "request to" + "\n"+address+"\n"+latitude+"/"+longitude+"\n" + "failed";  
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
            // Debug.Log( www.downloadHandler.text );
            request_status = "request" + GPS.Instance.latitude.ToString() + " \n" + GPS.Instance.longitude.ToString() + " SUCCESS.";
            //   request_status = "request to" + "\n"+address+"\n"+latitude+"/"+longitude+"\n" + "success"; 
            
            ProcessServerResponse(www.downloadHandler.text);
           
        }
    }

    void ProcessServerResponse(string rawResponse)
    {
        Debug.Log("test");
        JSONNode node = JSON.Parse(rawResponse);
        Debug.Log(rawResponse);
        if (node["response"].Count != 0 && !hasSpawned)
        {
            for (int i = 0; i < 10; i++)
            {
                string x = node["response"][i]["latitude"]["$numberDecimal"];
                string y = node["response"][i]["longitude"]["$numberDecimal"];
               
                string test_location = x + "," + y;
                Debug.Log(test_location);
                spawnOnMap._locationStrings.Add(test_location);
            }

            spawnOnMap.SpawnObject();
            hasSpawned = true;
        }
       
        

    }

    private void Update()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(GetWebData(remote_url));
    }
}