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
    }

    void FloatAndRotatePointer()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.fixedTime * Mathf.PI*frequency)*amplitude)+15, transform.position.z);
    }

    private void OnMouseDown()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().MushroomPosition.Set((float)eventPos.x, (float)eventPos.y, 0);
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
