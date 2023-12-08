using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class PlaneObjectSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] mushroom;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    [SerializeField]
    public int numberOfMushroom;
    public Text numberOfMushroomText;
    private int mushroomCounter = 0;
    public Text mushroomCounterText;

    // Contains the player's position (constantly updated)
    public PlayerLocation playerLocation;
    
    // List of all mushroom positions
    private MushroomPosition mushroomPositions;

    // How far away should a mushroom spawn
    [SerializeField]
    public float distance;
    public Text longitudeDifferenceText;
    public Text latitudeDifferenceText;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();

        playerLocation = GetComponent<PlayerLocation>();
        mushroomPositions = GetComponent<MushroomPosition>();

        numberOfMushroomText.text = numberOfMushroom.ToString();
    }

    // Find the mushroom that's closest to the player
    bool findCloseMushroom()
    {
        // Iterate through all possible mushroom positions
        for(int i = 0; i < mushroomPositions.numberAllTheMushroom - 1; i++) {
            // Check if player is close to mushroom
            double latitudeDifference = Math.Abs(playerLocation.latitudeValue - mushroomPositions.GetMushroom(i).latitude);
            double longitudeDifference = Math.Abs(playerLocation.longitudeValue - mushroomPositions.GetMushroom(i).longitude);
            
            latitudeDifferenceText.text = latitudeDifference.ToString("F5");
            longitudeDifferenceText.text = longitudeDifference.ToString("F5");

            // distance ... maximum distance a user is allowed to be away form a mushroom position and the mushroom still spawns
            if(latitudeDifference < distance && longitudeDifference < distance)
            {
                return true;
            }
        }
        
        return false; 
    }

    // Checks if the user touches the screen
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Spawns a mushroom on the plane the user touches
    void SpawnOnTouch()
    {
        // Check for user input
        if(!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        // Raycast to find plane
        if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(mushroomCounter < numberOfMushroom && mushroom != null) {
                // Resize mushroom
                int randomPrefab = UnityEngine.Random.Range(0, mushroom.Length);
                mushroom[randomPrefab].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                // TODO: make sure there is enough space between two mushrooms
                GameObject spawnedObject = Instantiate(mushroom[randomPrefab], hitPose.position, hitPose.rotation);
                spawnedObjects.Add(spawnedObject);
                mushroomCounter++;
                
                mushroomCounterText.text = mushroomCounter.ToString();
            }
        }
    }

    // Spawns a mushroom at the first plane a ray hits
    void RandomSpawn()
    {
        // Raycast to find plane
        if(_arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(mushroomCounter < numberOfMushroom && mushroom != null) {
                // Resize mushroom
                int randomPrefab = UnityEngine.Random.Range(0, mushroom.Length);
                mushroom[randomPrefab].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                // TODO: make sure there is enough space between two mushrooms
                GameObject spawnedObject = Instantiate(mushroom[randomPrefab], hitPose.position, hitPose.rotation);
                spawnedObjects.Add(spawnedObject);
                mushroomCounter++;

                mushroomCounterText.text = mushroomCounter.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerLocation.GPSStatus.text == "Running" && mushroomPositions.numberAllTheMushroom > 0)
        {
            // Check if a mushroom is near the player
            if(findCloseMushroom()) 
            {
                // Spawn mushroom
                //SpawnOnTouch();
                RandomSpawn();
            }
        }
    }
}
