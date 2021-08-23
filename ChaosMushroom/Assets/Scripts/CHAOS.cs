using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

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
