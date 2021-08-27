using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SpawnArea : MonoBehaviour
{
    public GameObject Trutle;
    //public GameObject SpawnAreaa;
    private Vector3 SpawnAreaPos;
    

    public int xPos;
    public int yPos;
    public int Maxcount;
    [SerializeField] private int Count;

    void Start ()
    {
        Vector3 SpawnArea = transform.position;
        //SpawnAreaPos = SpawnArea.transform.position;
        StartCoroutine(EnemyDrop());
        xPos = (int)Random.Range(43f,49f);
        yPos = (int)Random.Range(2f,5f);
        
    }
    IEnumerator EnemyDrop()
   {
       while (Count < Maxcount)
       {

           Instantiate(Trutle,new Vector3(xPos,yPos,-0.1f),Quaternion.identity);
           yield return new WaitForSeconds(0.1f);
           Count += 1;
       }
   }

}
