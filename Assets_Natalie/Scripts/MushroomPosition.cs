using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPosition : MonoBehaviour
{
    private MushroomPosition[] allTheMushroom;
    public int numberAllTheMushroom;

    public float mushroomLongitude;
    public float mushroomLatitude;
    public float mushroomAltitude;

    public MushroomPosition(float longitude, float latitude, float altitude)
    {
        mushroomLongitude = longitude;
        mushroomLatitude = latitude;
        mushroomAltitude = altitude;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: initialize mushroom positions
        allTheMushroom[0].mushroomLongitude;
        allTheMushroom[0].mushroomLatitude;
        allTheMushroom[0].mushroomAltitude;
        numberAllTheMushroom++;
    }

    public MushroomPosition GetMushroom(int i)
    {
        if(i < allTheMushroom.Length)
        {
            return allTheMushroom[i];
        }

        // max long: 180, max lat: 90, max alt: 1.000
        return new MushroomPosition(181, 91, 1001);
    }
}
