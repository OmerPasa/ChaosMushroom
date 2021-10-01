using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mole_EnemyorBoss : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public AIPath aiPath;
    [SerializeField]
    private GameObject Mole_BossAuto;
    [SerializeField]
    private float timeBtwAttack;
    [SerializeField]
    public float startTimeBtwAttack;
    private float damageDelay;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int health = 4;
    public int damage = 3;
    int Count;
    const string ENEMY_IDLE = "Mole_Idle";
    const string ENEMY_TAKEDAMAGE = "Mole_TakeDamage";
    const string ENEMY_DEATH = "Mole_Explode";
    const string ENEMY_ATTACK = "Mole_Attack";
    const string ENEMY_JUMP = "Mole_Jump";
    const string ENEMY_JUMPATTACK = "Mole_JumpAttack";
    const string ENEMY_MOVEMENT = "Mole_Movement";
    private Animator animator;
    private string currentAnimaton;
    private bool isAttacking;
    private bool isTakingDamage;
    private bool isDying;
    private void Start() 
    {
        animator = GetComponent<Animator>();
        //Rigidbody2D.constraints = RigidbodyConstraints.FreezePosition.z;
    }
    void FixedUpdate() 
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //tring clamp but in vain ? :/
        if (transform.position.z <= -0.01f || transform.position.z >= 0.01f)
        {
            //Vector3 clampedPosition = transform.position;
            float zPos = Mathf.Clamp(0, -1.00f, 1.00f);
            transform.position = new Vector3(transform.position.x , transform.position.y , zPos);
        }

       // trying to slow down movement
       //Rigidbody2D.velocity.x != 0.00f

        if (!isAttacking && !isTakingDamage && !isDying)
        {
            if (transform.hasChanged)
            {
                ChangeAnimationState(ENEMY_MOVEMENT);
                
            }
            else
            {
                ChangeAnimationState(ENEMY_IDLE);
                
            }
        }


        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        if (timeBtwAttack <= 0)
        {
        if (enemiesInRange.Length >= 1)
        {
            //for giving every one of enemies damage.
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
            isAttacking = true;
            ChangeAnimationState(ENEMY_ATTACK);
            damageDelay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AttackComplete", damageDelay);
            enemiesInRange[i].GetComponent<PlayerScript>().PlayerTakeDamage(damage);
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
        Debug.Log("ATTACKCOMPLETEBOSS");
    }
    // Callback to draw gizmos only if the object is selected.
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
            Debug.Log("MOLE_DIED");
            Invoke("Die",0.9f);
        }
    }

    void Die()
    {
        Destroy(Mole_BossAuto);
    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}

