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

    public Vector3 CurrentMushroom;

    [SerializeField]
    public GameObject[] mushroom;

    private List<GameObject> spawnedObjects = new List<GameObject>();


    // Contains the player's position (constantly updated)
    
    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
        RandomSpawn();
        new WaitForSeconds(10);
        //GameManager.Instance.MapNavigate = true;
        
    }

    void RandomSpawn()
    {
        // Raycast to find plane
        if(_arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if( mushroom != null) {
                // Resize mushroom
                int randomPrefab = UnityEngine.Random.Range(0, mushroom.Length);
                mushroom[randomPrefab].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Spawn mushroom
                // TODO: make sure there is enough space between two mushrooms
                GameObject spawnedObject = Instantiate(mushroom[randomPrefab], hitPose.position, hitPose.rotation);
                spawnedObjects.Add(spawnedObject);
               
              
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RandomSpawn();
    }
}
