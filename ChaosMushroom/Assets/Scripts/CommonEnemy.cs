using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CommonEnemy : MonoBehaviour
{
    public AIPath aiPath;
    [SerializeField]
    private GameObject EnemyTurtleAuto;
    [SerializeField]
    private float timeBtwAttack;
    [SerializeField]
    public float startTimeBtwAttack;
    [SerializeField]
    private Transform attackPos;
    public LayerMask whatIsEnemies;
    private float attackRange = 1;
    private float damageDelay;
    public int health = 4;
    public int damage = 1;
    int Count;
    const string ENEMY_IDLERUN = "Turtle_Idle-Run";
    const string ENEMY_DEATH = "Turtle_Explode";
    const string ENEMY_TAKEDAMAGE = "Turtle_TakeDamage";
    private Animator animator;
    public PlayerScript playerScript;
    private string currentAnimaton;
    private bool isAttacking;
    private bool isTakingDamage;
    
    private bool isDying;
    void Start() 
    {
        animator = GetComponent<Animator>();
        //PlayerScript = FindObjectOfType<PlayerScript>();
        //PlayerScript PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }
    void FixedUpdate() 
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
        if (!isAttacking && !isTakingDamage && !isDying)
        {
        ChangeAnimationState(ENEMY_IDLERUN);
        }
        // so why even though range is 0 and collider given to code is wayyy far  away this is working?
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        if (timeBtwAttack <= 0)            //making sure it isn't attaking always
        {
        if (enemiesInRange.Length >= 1)     // my collider detector 
        {
            Debug.Log(enemiesInRange.Length + "ENEMİES İN RANGE");
            Debug.Log("Player_In_Range!!");
            //for giving every one of enemies damage.
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
            isAttacking = true;
            playerScript.GetComponent<PlayerScript>().PlayerTakeDamage(damage);
            Invoke("AttackComplete", damageDelay);
            }
        }
        timeBtwAttack = startTimeBtwAttack;
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
        void AttackComplete()
    {
        isAttacking = false;
        Debug.Log("ATTACKCOMPLETE TURTLE");
    }
    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
        
    private void OnTriggerEnter2D (Collider2D collision)
    {
      if(collision.CompareTag("Bullet"))
    {
        isTakingDamage = true;
        Destroy(collision.gameObject);
        ChangeAnimationState(ENEMY_TAKEDAMAGE);
        health--;
        isTakingDamage = false;
        //damageDelay = animator.GetCurrentAnimatorStateInfo(0).length;
        //Invoke("DamageDelayComplete", damageDelay);
    }
     if (health <= 0)
        {
            isDying = true;
            ChangeAnimationState(ENEMY_DEATH);
            Invoke("Die",1f);
            
        }
    }
    /*void DamageDelayComplete()
    {
        isTakingDamage = false;
    }*/
    void Die()
    {
        Destroy(EnemyTurtleAuto);
    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
