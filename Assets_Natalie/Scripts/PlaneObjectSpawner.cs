using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaneObjectSpawner : MonoBehaviour
{
    public GameObject[] mushroom;

    private GameObject spawnedObject;
    public int numberOfMushroom;
    private int mushroomCounter = 0;

    public PlayerLocation playerLocation;
    private float playerLongitude;
    private float playerLatitude;
    private float playerAltitude;

    public float mushroomLongitude;
    public float mushroomLatitude;
    public float mushroomAltitude;

    // How far away should a mushroom spawn
    public float distance;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        // TODO: move to Update()?
        // Get the player's location
        playerLongitude = playerLocation.longitudeValue;
        playerLatitude = playerLocation.latitudeValue;
        playerAltitude = playerLocation.altitudeValue;
    }

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
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

            // TODO: why is mushroomCounter < numberOfMushroom not working
            if(spawnedObject == null && mushroom != null) {
                // Resize mushroom
                mushroom[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                spawnedObject = Instantiate(mushroom[0], hitPose.position, hitPose.rotation);
                mushroomCounter++;
            }
        }
    }

    void RandomSpawn()
    {
        // Raycast to find plane
        if(_arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            // Get a random position on plane
            //ARPlane plane = hits[0].trackable as ARPlane;
            ARPlane plane = null;
            Vector3 randomPosition = GetRandomPositionOnPlane(plane);

            // TODO: why is mushroomCounter < numberOfMushroom not working
            if(spawnedObject == null && mushroom != null) {
                // Resize mushroom
                mushroom[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                spawnedObject = Instantiate(mushroom[0], randomPosition, hitPose.rotation);
                mushroomCounter++;
            }
        }
    }

    Vector3 GetRandomPositionOnPlane(ARPlane plane)
    {
        // Get random position within the plane
        Vector2 planeExtents = plane.extents;

        float randomX = Random.Range(-planeExtents.x / 2f, planeExtents.x / 2f);
        float randomZ = Random.Range(-planeExtents.y / 2f, planeExtents.y / 2f);

        // calculate the height of the plane: Ax + By + Cz + D = 0
        Vector3 planePosition = plane.transform.position;
        Vector3 planeNormal = plane.transform.up;

        float height = -(planeNormal.x * planePosition.x + planeNormal.y * planePosition.y + planeNormal.z * planePosition.z) / planeNormal.y;
    
        return new Vector3(randomX + planePosition.x, height, randomZ + planePosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is close to mushroom
        float longitudeDifference = Mathf.Abs(playerLongitude -mushroomLongitude);
        float latitudeDifference = Mathf.Abs(playerLatitude - mushroomLatitude);

        if(longitudeDifference < distance && latitudeDifference < distance)
        {
            // TODO: doesn't work yet
        } 

        SpawnOnTouch();
        //RandomSpawn();
    }
}
