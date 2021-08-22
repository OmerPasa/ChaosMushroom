using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMovement : MonoBehaviour
{
    public GameObject main;
    public float distance = 0;
    int chaos;

    // Start is called before the first frame update
    void Start()
    {
        float distance = chaos;
    }

    // Update is called once per frame
    void Update()
    {
        float distances = Vector3.Distance (main.transform.position, transform.position);
        if (CHAOS.chaosValue - distance >= 0)
        {
        CHAOS.chaosValue - distance += chaos;
        }
    }
}
