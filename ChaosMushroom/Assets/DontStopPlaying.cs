using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStopPlaying : MonoBehaviour
{
    public float volumeBack;
    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}