using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : MonoBehaviour
{
    
    public int health = 100;
    public int count = 6;
    public GameObject deathEffect;
    // Start is called before the first frame update
    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <=0)
        {
            Die();
        }

    }
    void Start () 
    {
        while (count >= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            count -= count;
        }
    }
    void Die ()
    {
        Destroy(gameObject);
    }
}
