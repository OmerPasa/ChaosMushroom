
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    public Transform groundCheck;

    private Animator animator;
    private string currentAnimaton;
    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    [SerializeField]
    private float jumpForce = 850;
    private int groundMask;
    private bool isGrounded;
    private bool isAttackPressed;
    private bool isAttacking;
    private bool isntDead;
    private bool TakingDamage;
    AudioSource AfterFiringMusic;
    public AudioSource BackGroundM;
    public bool isFacingLeft = false;
    public Transform firePoint;
    public GameObject BulletPre;

    [SerializeField]
    private float attackDelay = 0.3f;
    public int Playerhealth;

    //Animation States
    const string PLAYER_IDLE = "Player_Idle_Gun";
    const string PLAYER_RUN = "Player_Movement_Gun";
    const string PLAYER_JUMP = "Player_Jump_Gun";
    const string PLAYER_ATTACK = "Player_Movement_Firing";
    const string PLAYER_AIR_ATTACK = "Player_Jump_Firing";
    const string PLAYER_DEATH = "Player_Death";
    const string PLAYER_TAKEDAMAGE = "Player_TakeDamage";

    //=====================================================
    // Start is called before the first frame update
    //=====================================================
    void Start()
    {
        GetComponent<Animation>()["Player_Movement_Firing"].wrapMode = WrapMode.Once;
        isntDead = true;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AfterFiringMusic = GetComponent<AudioSource>();
        AudioSource BackGroundM = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        BulletScriptt BulletScript = GameObject.Find("BulletPrefab").GetComponent<BulletScriptt>();
    }
    void Update()
    {   
        if (Playerhealth <= 0)
        {
            isntDead = false;
            ChangeAnimationState(PLAYER_DEATH);
            Invoke("Die", 2f);
        }
        //Checking for inputs
        xAxis = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            transform.localScale = new Vector3(1, 1, 1);
            isFacingLeft = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            transform.localScale = new Vector3(-1, 1, 1);
            isFacingLeft = true;
        }else if (isGrounded)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        
        
        //space jump key pressed?
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            isJumpPressed = true;
        }

        //space Atatck key pressed?
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftControl))
        {
            GameObject B = Instantiate(BulletPre,firePoint.position,firePoint.rotation);
            B.GetComponent<BulletScriptt>().StartShooting(isFacingLeft);
            AfterFiringMusic.Play();
            isAttackPressed = true;
        }
    }

    //=====================================================
    // Physics based time step loop
    //=====================================================
    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
            Debug.Log("isGROUNDED");
        }
        else
        {
            isGrounded = false;
        }

        //------------------------------------------
        
        if (isGrounded && !isAttacking && isntDead && !TakingDamage)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }


        //Check if trying to jump 
        if (isJumpPressed && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }

        //attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;

                if(isGrounded)
                {
                    //animator["PLAYER_ATTACK"].wrapMode = WrapMode.Once;
                    ChangeAnimationState(PLAYER_ATTACK);
                    BackGroundM.volume = 0.0f;
                    AfterFiringMusic.Play();
                }
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                    BackGroundM.volume = 0.0f;
                }
                Invoke("AttackComplete", attackDelay);
            }
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void PlayerTakeDamage(int damage)
    {
        Playerhealth -= damage;
        TakingDamage = true;
        Debug.Log("damageTaken");
        ChangeAnimationState(PLAYER_TAKEDAMAGE);
        TakingDamage = false;

        //play Player taken damage
    }
    void AttackComplete()
    {
        BackGroundM.volume = 1f;
        isAttacking = false;
    }

    //=====================================================
    // mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}
