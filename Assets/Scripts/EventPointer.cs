using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Utils;
public class EventPointer : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float amplitude = 2.0f;
    [SerializeField] float frequency = 0.50f;

    LocationStatus playerLocation;
    public Mapbox.Utils.Vector2d eventPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FloatAndRotatePointer();

        if (Input.touchCount > 0)
        {
            // Loop through all the touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                // Get the current touch
                Touch touch = Input.GetTouch(i);

                // Check if the touch is over the current GameObject
                if (IsTouchOverObject(touch))
                {
                    // Perform actions when the GameObject is touched
                    HandleTouch();
                }
            }
        }
    }
    bool IsTouchOverObject(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        // Cast a ray and check if it hits the GameObject
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == gameObject;
        }

        return false;
    }
    void HandleTouch()
    {
        // Do something when the GameObject is touched
        Debug.Log("Object Touched!");
        GameObject.Find("GameManager").GetComponent<GameManager>().MushroomPosition.Set((float)eventPos.x, (float)eventPos.y, 0);
        GameObject.Find("GameManager").GetComponent<GameManager>().openMushroom = true;
    }
    void FloatAndRotatePointer()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.fixedTime * Mathf.PI*frequency)*amplitude)+15, transform.position.z);
    }

    private void OnMouseDown()
    {
        
        //playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        //var currentPlayerlocation = new GeoCoordinatePortable.GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLon());
        //var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPos[0], eventPos[1]);
        //var distance = currentPlayerlocation.GetDistanceTo(eventLocation);
        //if(distance < 70)
        //{
        //    Debug.Log("In distance"); //change color or smth
        //}
        //Debug.Log(distance);
        //Debug.Log("Cliiiicked");
    }
}
