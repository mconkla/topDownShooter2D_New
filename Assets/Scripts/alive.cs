using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alive : MonoBehaviour
{
    [HideInInspector]
    public bool dead = false;
    
    [HideInInspector]
    public bool punched = false;

    public gunObject mygun;


    float knockedTime;

    AudioManager audioManager;

    [HideInInspector]
    public Transform child;

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

        if (dead)
        {
            
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

            if (mygun != null)
            {
                gameObject.transform.DetachChildren();
                mygun = null;
            }
          


        }
      
        
        
          
        
        
    }

    private void FixedUpdate()
    {
       
        if (mygun != null && !dead)
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<PlayerMovement>().punch)
        {
            punched = true;
            audioManager.PlaySound("enemy");
            knockedTime =   1.5f;
        }
        else
        {
            punched = false;
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

}
