using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
public class DistanceMovement : MonoBehaviour
{
    public GameObject main;
    public float distance = 0;
    //int chaos;

    // Start is called before the first frame update
    void Start()
    {
        CHAOS.chaosValue = (int)distance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance (main.transform.position, transform.position);  // kendisiyle ve ana obje arasındaki fark hesaplanır
        
        Console.WriteLine(distance);
        
        /*if (CHAOS.chaosValue - distance >= 0) //artan chaos puanı 
        {
        CHAOS.chaosValue - distance += chaos;
        }*/
    }
}
