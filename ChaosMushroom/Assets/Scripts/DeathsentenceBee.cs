using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathsentenceBee : MonoBehaviour
{
    private Animator animator;
    const string ENEMY_DEATH = "Bee_Explode";
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("ENEMY_DEATH");
        new WaitForSeconds(2f);
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
