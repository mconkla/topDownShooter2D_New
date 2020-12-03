using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject feet;
    AnimationSkript myAnimator;
    public CharacterController2D controllPlayer;
    public Animator animator;
    public followPlayer cameraMovement;

    [Range(10f, 100f)]
    public float runSpeed;

    [HideInInspector]
    public bool alive = true;
    [HideInInspector]
    public bool isWalking = false;
    [HideInInspector]
    public gunObject mygun;
    [HideInInspector]
    public granateSkript myGranate;
    [HideInInspector]
    public float Horizontal = 0f, Vertical = 0f;
    [HideInInspector]
    public bool punch = false;

    AudioManager audiomanager;

    public GameObject gunCheck;

    private void Awake()
    {
        myAnimator = FindObjectOfType<AnimationSkript>();
        audiomanager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
       

        if (alive)
        {
            
                    
                if (mygun != null && mygun.inHand)
                {

                    switch (mygun.name)
                    {
                        case "uzi":
                            myAnimator.showUzi();
                            break;
                        case "gun":
                            myAnimator.showGun();
                            break;
                        case "mp":
                            myAnimator.showMp();
                            break;
                       
                }
                    if (!mygun.singleShoot)
                    {

                        if (Input.GetMouseButton(0))
                        {

                            mygun.shoot = true;


                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                            mygun.shoot = true;
                    }


                }
            

            else
            {
                noWeaponEquipt();
            }
            

            checkMoveInput();

            if (GetComponentInChildren<gunCheck>().mygun != null)
               mygun = GetComponentInChildren<gunCheck>().mygun;

        }
        else
        {
            deadfunction();
        }


        
    }


 

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraMovement.shift = true;
        }

        isWalking = !(Horizontal == 0 && Vertical == 0);

        FindObjectOfType<AnimationSkript>().animator.SetFloat("walking",Mathf.Abs(Horizontal + Vertical));

        controllPlayer.Move(Horizontal * Time.fixedDeltaTime, Vertical * Time.fixedDeltaTime);

    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("guard"))
        {
            if (punch) collision.gameObject.GetComponent<alive>().punched = true;

        }
    }


        void deadfunction()
    {
        myAnimator.showDead();
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        feet.GetComponent<SpriteRenderer>().enabled = false;

    }


    void checkMoveInput()
    {
        //Sensitivity Handling of Joystick - Horizontal Movement
        if (Input.anyKey)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                Horizontal = runSpeed;

            }
            else if (Input.GetAxis("Horizontal") < -0f)
            {
                Horizontal = -runSpeed;

            }
            else if (Input.GetAxis("Horizontal") == 0f)
            {
                Horizontal = 0f;
                
            }
            //Sensitivity Handling of Joystick  - Horizontal Movement

            if (Input.GetAxis("Vertical") > 0f)
            {
                Vertical = runSpeed;

            }
            else if (Input.GetAxis("Vertical") < 0f)
            {
                Vertical = -runSpeed;

            }
            else if(Input.GetAxis("Vertical") == 0f)
            {
                Vertical = 0f;
            }
           
        }
        else
        {
            Horizontal = 0f;
            Vertical = 0f;
        }
    }

    void noWeaponEquipt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Random.Range(0, 2) == 0)
            {
                myAnimator.leftPunch();
            }
            else
            {
                myAnimator.rightPunch();
            }
            punch = true;
            audiomanager.PlaySound("Punch");
        }
      
        else
        {
            myAnimator.showIdle();
            punch = false;
        }
    }

}

