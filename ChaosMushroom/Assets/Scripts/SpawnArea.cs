﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SpawnArea : MonoBehaviour
{
    private bool collusionhappened;
    public GameObject SpawnedObject;
    public Transform SpawningPlace;
    int xPos;
    int yPos;
    [SerializeField]
    private int Maxcount;
    [SerializeField]
     private int Count;
     private CheckPointSystem gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gm").GetComponent<CheckPointSystem>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered!!!!");
    if(other.gameObject.tag == "Player")
    {
        gm.lastCheckPointPos = transform.position;
        if (collusionhappened)
        {
            return;   
        }else
        {
        xPos = (int)SpawningPlace.transform.position.x;
        yPos = (int)SpawningPlace.transform.position.y;
        StartCoroutine(EnemyDrop());
        collusionhappened = true;      
        }
          
    }
    }
    IEnumerator EnemyDrop()
   {
       for (Count = 0; Count < Maxcount; Count++)
       {
           Instantiate(SpawnedObject,new Vector3(xPos,yPos,0f),Quaternion.identity);
           yield return new WaitForSeconds(0.1f);
       }
       /*
       if (Count == Maxcount)
        {
            yield return null;
        }else
        {
            Instantiate(Trutle,new Vector3(xPos,yPos,0f),Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

        */
    }


}
