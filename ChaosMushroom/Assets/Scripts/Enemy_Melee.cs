using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Melee : MonoBehaviour
{
    public float viewRange;
    public float minRange;
    public float closeAttackRange;
    public float closeAttackTime;
    public float bulletRange;
    public float bulletTime;
    public float movementSpeed;
    public float jumpPower;
    public float jumpTime;
    public GameObject character;
    public Transform Character;
    public Transform EyeRay;
    public Transform MidRay;
    public GameObject bullet;

    float closeATime2 = 0;
    float bulletTime2 = 0;
    float jumpTime2 = 0;
    float distance;
    bool grounded = true;
    bool pathBlocked = false;
    bool StopMoving;

    float tempX = 10000;
    float tempY = 0;
    public Rigidbody2D Rigidbody2D;
    public AIPath aiPath;
    [SerializeField]private GameObject Mole_BossAuto;
    [SerializeField]private float timeBtwAttack;
    [SerializeField]public float startTimeBtwAttack;
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
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //Rigidbody2D.constraints = RigidbodyConstraints.FreezePositionZ;
    }
    void Update() 
    {
        //deneme
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.right, Color.red, 1000);

        //===================================================
        //flipping code
        if(transform.position.x < Character.position.x)
        {
            //turn object
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.position.x > Character.position.x)
        {
            //turn object ro other side
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        //=====================================================================
        //Raycast 
        var castDist = distance;

        Vector2 endPos = MidRay.position + Vector3.right * 10; 
        RaycastHit2D Midray = Physics2D.Linecast(MidRay.position, endPos , 1 << LayerMask.NameToLayer("Ground"));
        
        if (Midray.collider != null)
        {
            if (Midray.collider.gameObject.CompareTag("Ground"))
            {
                pathBlocked = true;
            }
        }
        print("START" + MidRay.position);
        print("FİNİSH" + endPos);
        //Drawing line
        Debug.DrawLine(MidRay.position,endPos, Color.green,Time.deltaTime * 10);
        
        Vector2 endPos1 = EyeRay.position + Vector3.right * 10; 
        RaycastHit2D Eyeray = Physics2D.Linecast(EyeRay.position, endPos1 , 1 << LayerMask.NameToLayer("Ground"));
        
        if (Eyeray.collider != null)
        {
            if (Eyeray.collider.gameObject.CompareTag("Ground"))
            {
                pathBlocked = true;
            }
        }
        //drawing line
        Debug.DrawLine(EyeRay.position,endPos1, Color.green,Time.deltaTime * 10);
        //================================================================

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_TakeDamage"))
        {
            StopMoving = true;
        }else
        {
            StopMoving = false;
        }
        Vector3 karPos = character.transform.position;
        Vector3 pos = transform.position;
        if (Mathf.Abs(karPos.x - pos.x)< viewRange) 
        {
            if (!StopMoving)
            {
                moveTowardCharacter(karPos, pos);
            }
            
        }
        if (Mathf.Abs(karPos.x - pos.x) < closeAttackRange)
        {
            //closeAttack();
        }
        else if (Mathf.Abs(karPos.x - pos.x) < bulletRange)
        {
           // shoot(karPos, pos);
        }


        if (pos.y != tempY) { grounded = false; } 
        else { grounded = true; }

        if (!isAttacking && !isTakingDamage && !isDying)
        {
            if (Rigidbody2D.velocity.x != 0.00f)
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
    void moveTowardCharacter(Vector3 karPos, Vector3 pos)
    {
        if (Mathf.Abs(karPos.x - pos.x) > minRange && !(pathBlocked&&grounded))
        {
            Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((karPos.x - pos.x) * movementSpeed / Mathf.Abs(karPos.x - pos.x), rb2d.velocity.y);

            //transform.localScale = new Vector3((karPos.x - pos.x) / Mathf.Abs(karPos.x - pos.x), 1, 1);
        }
        else if ((pathBlocked && grounded))
        {
            jump();
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
    /*
    void Path_Blocked()
    {
        var castDist = distance;

        Vector2 endPos = MidRay.position + Vector3.right * distance; 
        RaycastHit2D Midray = Physics2D.Linecast(MidRay.position, endPos , 1 << LayerMask.NameToLayer("Ground"));
        
        if (Midray.collider != null)
        {
            if (Midray.collider.gameObject.CompareTag("Ground"))
            {
                pathBlocked = true;
            }
        }
        print("START" + MidRay.position);
        print("FİNİSH" + Midray.point);
        Debug.DrawLine(MidRay.position, Midray.point, Color.green);
        
        Vector2 endPos1 = EyeRay.position + Vector3.right * distance; 
        RaycastHit2D Eyeray = Physics2D.Linecast(EyeRay.position, endPos1 , 1 << LayerMask.NameToLayer("Ground"));
        
        if (Eyeray.collider != null)
        {
            if (Eyeray.collider.gameObject.CompareTag("Ground"))
            {
                pathBlocked = true;
            }
        }
        Debug.DrawLine(EyeRay.position,Eyeray.point, Color.green);
    }
    
    */
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            grounded = true;
            Debug.Log("PATHBLOCKED" + "GROUNDED");
            jump();
        }
      if(collision.CompareTag("Bullet"))
    {
        isTakingDamage = true;
        Destroy(collision.gameObject);
        ChangeAnimationState(ENEMY_TAKEDAMAGE);
        health--;
        damageDelay = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("DamageDelayComplete", damageDelay);
    }
     if (health <= 0)
        {
            isDying = true;
            ChangeAnimationState(ENEMY_DEATH);
            Debug.Log("MOLE_DIED");
            Invoke("Die",0.9f);
        }
    }
    void DamageDelayComplete()
    {
        isTakingDamage = false;
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
    void jump ()
    {
        if (jumpTime - (Time.realtimeSinceStartup - jumpTime2) <= 0 )
        {
            jumpTime2 = Time.realtimeSinceStartup;

            gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, jumpPower);

        }
    }

    void closeAttack()
    {
        //gettingDamage
        //animation
        if (closeAttackTime - (Time.realtimeSinceStartup - closeATime2) <= 0)
        {
            Debug.Log("hit");
            closeATime2 = Time.realtimeSinceStartup;
        }
    }

    void shoot(Vector3 karPos, Vector3 pos)
    {

        if (bulletTime - (Time.realtimeSinceStartup - bulletTime2) <= 0)
        {
            bulletTime2 = Time.realtimeSinceStartup;
            Debug.Log("fire");//ate�

            Vector3 a = (karPos - pos);
            GameObject bulletClone = (GameObject)Instantiate(bullet, pos + transform.right * 0.5f + new Vector3(0, 0, -0.21f), transform.rotation);
            bulletClone.transform.up = new Vector3(a.x, a.y, 0); 
            bulletClone.GetComponent<BulletScriptt>().StartShooting(a.x<0);


        }
    }
    void OnTriggerExit2D(Collider2D temas)
    {
        if (temas.tag == "block")
        {
            pathBlocked = false;
        }
    }
}

