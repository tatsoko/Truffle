using Mapbox.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Add any variables or methods you want to share between scenes here
    public Vector3 MushroomPosition;
    public Vector3 PlayerPosition;
    public bool MapNavigate;
    //float latitude = 48.26265f;
    //float longitude = 11.66808f;
    bool CheckIfClose(Vector3 source, Vector3 destination, float distance)
    {

            // Check if player is close to mushroom
            double latitudeDifference = Math.Abs(source.x - destination.x);
            double longitudeDifference = Math.Abs(source.y - destination.y);

            if (latitudeDifference < distance && longitudeDifference < distance)
            {
                return true;
            }

        return false;
    }

   
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        LocationStatus playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        //PlayerPosition = new Vector3(latitude,longitude, 0.0f);
        PlayerPosition =  new Vector3(((float)playerLocation.GetLocationLat()), ((float)playerLocation.GetLocationLon()), 0.0f);

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Map")
        {
            if (CheckIfClose(PlayerPosition, MushroomPosition, 0.000000001f))
            {
                SceneManager.LoadScene("TrufflePlane");
            }
        }
        else
        {
            if (MapNavigate)
            {
                SceneManager.LoadScene("Map");
                MapNavigate = false;
            }
        }
    }

}