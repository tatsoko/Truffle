using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPSTextUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coordinates; 
   
    private void Update()
    {
        coordinates.text = "Lat:" + GPS.Instance.latitude.ToString() + " Log:" + GPS.Instance.longitude.ToString();
        
    }
}
