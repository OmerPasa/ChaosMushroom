
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
    Transform groundCheck;

    private Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    [SerializeField]
    private float jumpForce = 850;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimaton;
    private bool isAttackPressed;
    private bool isAttacking;
    AudioSource AfterFiringMusic;
    public AudioSource BackGroundM;
    public bool isFacingLeft;
    public Transform firePoint;
    public GameObject BulletPre;
    bool fireStartedMusic = false;

    [SerializeField]
    private float attackDelay = 0.3f;

    //Animation States
    const string PLAYER_IDLE = "Player_Idle_Gun";
    const string PLAYER_RUN = "Player_Movement_Gun";
    const string PLAYER_JUMP = "Player_Jump_Gun";
    const string PLAYER_ATTACK = "Player_Movement_Firing";
    const string PLAYER_AIR_ATTACK = "Player_Jump_Firing";

    //=====================================================
    // Start is called before the first frame update
    //=====================================================
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AfterFiringMusic = GetComponent<AudioSource>();
        AudioSource BackGroundM = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        GameObject.Find("BulletPrefab").GetComponent<BulletScriptt>();
       // volumeBack volumeBack = gameObject.GetComponent<float>();
       // groundMask = 1 << LayerMask.NameToLayer("Ground");
       AfterFiringMusic.Play();
       AfterFiringMusic.volume = 0.0f;

    }

    //=====================================================
    // Update is called once per frame
    //=====================================================
    void Update()
    {
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
            isAttackPressed = true;

            /*
            
            Instantiate(BulletPre,firePoint.position,firePoint.rotation);

            
            BulletPre.transform.position = firePoint.transform.position;
            */
        }
        if (!fireStartedMusic) 
        {
            AfterFiringMusic.volume = 1f;
            fireStartedMusic = true;
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
        }
        else
        {
            isGrounded = false;
        }

        //------------------------------------------
        /*
        //Check update movement based on input
        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);

        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            vel.x = 0;
            
        }

        //assign the new velocity to the rigidbody
        rb2d.velocity = vel;
        */
        if (isGrounded && !isAttacking)
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

        //------------------------------------------

        //Check if trying to jump 
        if (isJumpPressed && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }


        /*
        if  (rb2d.velocity.x < -1)
        {
            transform.Rotate(0f, 180f, 0f);
        }else if (rb2d.velocity.x > 1)
        {
            transform.Rotate(0f, 0f, 0f);
        }else
        {
            transform.Rotate(0f, 180f, 0f);
        }*/

        //attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;

                if(isGrounded)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                    fireStartedMusic = true;
                    BackGroundM.volume = 0.0f;

                }
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                    fireStartedMusic = true;
                    BackGroundM.volume = 0.0f;
                }
                Invoke("AttackComplete", attackDelay);
            }
        }
    }
    void AttackComplete()
    {
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
