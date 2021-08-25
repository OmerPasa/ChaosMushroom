using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
      CommonEnemy enemy = hitInfo.GetComponent<CommonEnemy>();
      if (enemy != null)
      {
          enemy.TakeDamage(50);
      }
      Destroy(gameObject);
      
    }

}
