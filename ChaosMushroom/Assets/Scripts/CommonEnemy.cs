using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : MonoBehaviour
{
    
    public int health = 2;
    int Count;
    public GameObject deathEffect;
    // Start is called before the first frame update
    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <=0)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
      if(collision.CompareTag("Bullet"))
    {
        Destroy(collision.gameObject);
        health--;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    }
}
