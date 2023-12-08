using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPointCloudManager))]
public class PointCloudObjectSpawner : MonoBehaviour
{
    public GameObject[] mushroom;

    private GameObject[] spawnedObject;
    public int numberOfMushroom = 1;
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

    private ARPointCloudManager _arPointCloudManager;

    static List<Vector3> points = new List<Vector3>();

    private void Awake()
    {
        _arPointCloudManager = GetComponent<ARPointCloudManager>();
    }

    void Start()
    {
        // Register for point cloud updated event
        _arPointCloudManager.pointCloudsChanged += OnPointCloudChanged;

        // TODO: move to Update()?
        // Get the player's location
        playerLongitude = playerLocation.longitudeValue;
        playerLatitude = playerLocation.latitudeValue;
        playerAltitude = playerLocation.altitudeValue;
    }

    private void OnPointCloudChanged(ARPointCloudChangedEventArgs eventArgs)
    {
        // Clear the previous points
        points.Clear();

        foreach (var pointCloud in eventArgs.added)
        {
            // Add all the points from the added point cloud
            points.AddRange(pointCloud.positions);
        }
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

        if (points.Count > 0)
        {
            // Choose a random point from the point cloud
            Vector3 randomPoint = points[Random.Range(0, points.Count)];

            // Spawn the object at the selected position
            if (mushroomCounter < numberOfMushroom && mushroom != null)
            {
                // Resize mushroom
                mushroom[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                spawnedObject[mushroomCounter] = Instantiate(mushroom[0], randomPoint, Quaternion.identity);
                mushroomCounter++;
            }
        }
    }
}
