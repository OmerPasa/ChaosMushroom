using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SpawnArea : MonoBehaviour
{
    private bool collusionhappened;
    public GameObject Trutle;
    public Transform EnemySpawner;
    int xPos;
    int yPos;
    public int Maxcount;
    [SerializeField]
     private int Count;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collusionhappened)
        {
            return;   
        }else
        {
        xPos = (int)EnemySpawner.position.x;
        yPos = (int)EnemySpawner.position.y;
        StartCoroutine(EnemyDrop());
        collusionhappened = true;      
        }
          
    }
    IEnumerator EnemyDrop()
   {
       if (Count == Maxcount)
        {
            yield return null;
        }else
        {
            Instantiate(Trutle,new Vector3(xPos,yPos,0f),Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Count += 1;
        }
    }
}
