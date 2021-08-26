using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusics : MonoBehaviour
{
     
     public AudioSource _AudioSource;
 
     public AudioClip _AudioClip1;
     public AudioClip _AudioClip2;
     bool fireStartedMusic = false;

     
 
     void Start() 
     {
 
         _AudioSource.clip = _AudioClip1;
 
         _AudioSource.Play();
     
     }
     
 
    void Update () 
    {
 
         if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftControl))
        {
            fireStartedMusic = true;
 
            if (_AudioSource.clip == _AudioClip1)
            {
                if (fireStartedMusic)
                {
                    _AudioSource.clip = _AudioClip2;
                    fireStartedMusic = true;
                }
            }else
            {
                 
                _AudioSource.clip = _AudioClip1;
                 
                _AudioSource.Play();
 
            }
 
        }
        if (!_AudioSource.isPlaying)
        {
                _AudioSource.clip = _AudioClip1;
                 
                _AudioSource.Play();
        }
     
     }
 
 }
