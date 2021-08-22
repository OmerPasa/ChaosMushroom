using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMovement : MonoBehaviour
{
    public int chaos = 0;
    public GameObject main;
    public float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        float distance = chaos;
    }

    // Update is called once per frame
    void Update()
    {
        float distances = Vector3.Distance (main.transform.position, object2.transform.position);
        if (chaos - distance >= 0)
        {
            chaos - distance += distances;
        }
    }
}
