using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStopPlaying : MonoBehaviour
{
    AudioSource audioData;
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Debug.Log("started");
    }
    public float volumeBack;
    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}