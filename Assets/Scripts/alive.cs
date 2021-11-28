using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alive : MonoBehaviour
{
    public bool dead = false;
    
    [HideInInspector]
    public bool punched = false;

    public gunObject mygun;


    float knockedTime;

    AudioManager audioManager;

    [HideInInspector]
    public Transform child;

    public PlayerMovement player;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (mygun != null)
        {
            mygun.inHand = true;
            mygun.Owner = gameObject;
            switch (mygun.name)
            {
                case "uzi":
                    gameObject.GetComponent<Animator>().SetBool("uzi", true);
                    break;
                case "gun":
                    gameObject.GetComponent<Animator>().SetBool("gun", true);
                    break;
                case "mp":
                    gameObject.GetComponent<Animator>().SetBool("mp", true);
                    break;
            }


        }

        child = GetComponentInChildren<Transform>();
     
    }

    void Update()
    {      
          
        
    }

    private void FixedUpdate()
    {
       
        if (mygun != null && !this.dead)
        {
          

            if (punched)
            {
                
                gameObject.GetComponent<Animator>().SetBool("gun", false);
                gameObject.GetComponent<Animator>().SetBool("mp", false);
                gameObject.GetComponent<Animator>().SetBool("uzi", false);
                if (mygun != null)
                {
                 
                    mygun.switchHandState();
                    mygun = null;
                }
                
            }

        }




    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.dead && collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            this.player = collision.gameObject.GetComponent<PlayerMovement>();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!this.dead && collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            
            if (player != null && player.punch)
            {
                punched = true;
                audioManager.PlaySound("enemy");
                knockedTime = 1.5f;
            }
            
        }
        else
        {
            punched = false;
            this.player = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!this.dead && collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            this.player = null;          
        }

    }


    public void shooting()
    {
     
        if(mygun != null)
            mygun.shootBullet();

    }

    void knockedState()
    {
        if (knockedTime > 0f)
        {
            gameObject.GetComponent<Animator>().SetBool("knocked", true);
            gameObject.GetComponentInParent<EnemyAI>().enabled = false;
            gameObject.GetComponentInParent<AIController>().enabled = false;
            gameObject.GetComponentInParent<pathSkript>().enabled = false;

            knockedTime -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("knocked", false);
            knockedTime = 0f;
            gameObject.GetComponentInParent<EnemyAI>().enabled = true;
            gameObject.GetComponentInParent<AIController>().enabled = true;
            //gameObject.GetComponentInParent<pathSkript>().enabled = true;
            return;
        }
    }


    public void setDead(bool deadState, string killer)
    {
        if (this.dead == deadState)
            return;

        this.dead = deadState;
        Debug.Log("Dead by " + killer);
        if (child.childCount > 0)
        {
            audioManager.PlaySound("enemy");
            Debug.Log("stirb");
            child.GetChild(0).gameObject.SetActive(false);
        }


        if (mygun != null)
            mygun.switchHandState();

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -5;
        gameObject.GetComponent<Animator>().SetBool("gun", false);
        gameObject.GetComponent<Animator>().SetBool("mp", false);
        gameObject.GetComponent<Animator>().SetBool("uzi", false);

        gameObject.GetComponent<Animator>().SetBool("dead", true);

        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        if (mygun != null)
        {
            gameObject.transform.DetachChildren();
            mygun = null;
        }


        gameObject.GetComponentInParent<EnemyAI>().enabled = false;
        gameObject.GetComponentInParent<pathSkript>().enabled = false;
        gameObject.GetComponentInParent<CircleCollider2D>().isTrigger = true;
        gameObject.GetComponentInParent<AIController>().enabled = false;

    }

}
