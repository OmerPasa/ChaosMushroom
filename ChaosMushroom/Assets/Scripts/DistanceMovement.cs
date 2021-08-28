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
    public int distance = 0;
    void Update()
    {
        int distance = (int)Vector3.Distance (main.transform.position, transform.position);  // kendisiyle ve ana obje arasındaki fark hesaplanır
        CHAOS.chaosValue = (int)distance;
        Console.WriteLine(distance);
        
        /*if (CHAOS.chaosValue - distance >= 0) //artan chaos puanı 
        {
        CHAOS.chaosValue - distance += chaos;
        }*/
    }
}
