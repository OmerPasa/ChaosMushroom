using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance (object1.transform.position, object2.transform.position);

    }
}
