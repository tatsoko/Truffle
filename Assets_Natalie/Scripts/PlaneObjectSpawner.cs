using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

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
    
    // Array of all mushroom positions
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

        numberOfMushroomText.text = numberOfMushroom.ToString();
    }

    bool findCloseMushroom()
    {
        // TODO: get mushroom locatioins
        // Iterate through all possible mushroom positions
        for(int i = 0; i < mushroomPositions.numberAllTheMushroom; i++) {
            // Check if player is close to mushroom
            float longitudeDifference = Mathf.Abs(playerLocation.longitudeValue - mushroomPositions.GetMushroom(i).mushroomLongitude);
            float latitudeDifference = Mathf.Abs(playerLocation.latitudeValue - mushroomPositions.GetMushroom(i).mushroomLatitude);

            longitudeDifferenceText.text = longitudeDifference.ToString();
            latitudeDifferenceText.text = latitudeDifference.ToString();

            if(longitudeDifference < distance && latitudeDifference < distance)
            {
                // TODO: doesn't work yet
                return true;
            }
        }
        
        return false; 
    }

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

    void SpawnOnTouch()
    {
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
                mushroom[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                // TODO: make sure there is enough space between two mushrooms
                GameObject spawnedObject = Instantiate(mushroom[0], hitPose.position, hitPose.rotation);
                spawnedObjects.Add(spawnedObject);
                mushroomCounter++;
                
                mushroomCounterText.text = mushroomCounter.ToString();
            }
        }
    }

    void RandomSpawn()
    {
        // Raycast to find plane
        if(_arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(mushroomCounter < numberOfMushroom && mushroom != null) {
                // Resize mushroom
                mushroom[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                // TODO: make sure there is enough space between two mushrooms
                GameObject spawnedObject = Instantiate(mushroom[0], hitPose.position, hitPose.rotation);
                spawnedObjects.Add(spawnedObject);
                mushroomCounter++;

                mushroomCounterText.text = mushroomCounter.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a mushroom is near the player
        // TODO: doesn't work yet
        //findCloseMushroom();

        // Spawn mushroom
        //SpawnOnTouch();
        RandomSpawn();
    }
}
