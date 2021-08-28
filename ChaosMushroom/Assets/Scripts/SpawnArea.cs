using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SpawnArea : MonoBehaviour
{
    public GameObject Trutle;
    public Transform 
    Vector3 EnemySpawner = transform.position;
    

    public int xPos;
    public int yPos;
    public int Maxcount;
    [SerializeField] private int Count;

    void Start ()
    {
        Vector3 EnemySpawner = transform.position;
        Vector3 SpawnAreaPos = transform.position;
        //SpawnAreaPos = SpawnArea.transform.position;
        StartCoroutine(EnemyDrop());
        xPos = (int)Random.Range(16f,30f);
        yPos = (int)Random.Range(-1f,1f);
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
