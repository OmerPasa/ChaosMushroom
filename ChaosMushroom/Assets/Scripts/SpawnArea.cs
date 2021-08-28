using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SpawnArea : MonoBehaviour
{
    public GameObject Trutle;
    public Transform EnemySpawner;
    int xPos;
    int yPos;
    public int Maxcount;
    [SerializeField] private int Count;

    void Start ()
    {
        //SpawnAreaPos = SpawnArea.transform.position;
        xPos = (int)EnemySpawner.position.x;
        yPos = (int)EnemySpawner.position.y;
        StartCoroutine(EnemyDrop());
        //xPos = (int)Random.Range(16f,30f);
        //yPos = (int)Random.Range(-1f,1f);
        //Transform EnemySpawner = Vector3(xPos, yPos, 0);
    }

    IEnumerator EnemyDrop()
   {
       while (Count < Maxcount)
       {

           Instantiate(Trutle,new Vector3(xPos,yPos,0f),Quaternion.identity);
           yield return new WaitForSeconds(0.1f);
           Count += 1;
       }
   }

}
