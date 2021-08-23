using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHAOS : MonoBehaviour
{
    public static int chaosValue = 0;
    Text chaos;
    void Start()
    {
        chaos = GetComponent<Text>();
    }

    
    void Update()
    {
        chaos.text = "Chaos: " + chaosValue;
    }
}
