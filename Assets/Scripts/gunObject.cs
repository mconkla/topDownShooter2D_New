using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class gunObject : MonoBehaviour
{
    //every gun has a SpriteRenderer and an Animatior and a certain Bullet to spawn
    public SpriteRenderer mySpriteRenderer;
    public Animator myAnimator;
    public GameObject bullet;

    public string ammoType = "";

    [HideInInspector]
    public int startMag = 0;
    //gun attributes
    public int magazin;
    public bool singleShoot;
    [Range(0f, 2f)]
    public float triggerDelay;

    [HideInInspector]
    public GameObject spawnedBullet;

    [HideInInspector]
    public GameObject Owner;

    [HideInInspector]
    public bool inHand = false;

    [HideInInspector]
    public bool shoot = false;

    public float bulletSpeed;

    AudioManager audioManager;


    //Delay for shooting bullets and time for being throwed
    [HideInInspector]
    public float waitingTime;
    float flytime = 0.0f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        //to refill to start amount
        startMag = magazin;
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        if (inHand)
        {
            //stick to your owner
            gameObject.GetComponent<Collider2D>().enabled = false;
            //transform.position = Camera.main.ScreenToWorldPoint(Owner.GetComponent<SpriteRenderer>().sprite.pivot);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(Owner.transform);
            transform.position = transform.parent.position ;
            //transform.position = Owner.transform.position;

            //make yourself unvisible
            mySpriteRenderer.enabled = false;
            myAnimator.enabled = false;

            //if owner shoots, bullet will be spawned
            if (shoot)
                shootBullet();
            

            //delay for weapon to spawn new bullets
            if (waitingTime > 0) waitingTime -= Time.deltaTime;

          
        }
       
        if (!inHand && Owner != null)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            transform.parent = null;
            //make yourself visible
            mySpriteRenderer.enabled = true;
            myAnimator.enabled = true;

            whoWasOwner(Owner.tag == "Player");
            
        }




    }




    public void shootBullet()
    {
        //is mag not empty and is delay finished ?
        if (magazin > 0 && waitingTime <= 0)
        {
            audioManager.PlaySound(gameObject.name);
            magazin--;

            //create Bullet
            Spawn();
            
            if (Owner.gameObject.tag == "Player")
            {
                FindObjectOfType<ShakeBehavior>().TriggerShake();
            }

            waitingTime = triggerDelay;
            shoot = false;
        }
        

    }


    void Spawn()
    {
       
      spawnedBullet = Instantiate(bullet, transform.position, Owner.transform.rotation, Owner.transform);
       
    }

    public void switchHandState()
    {
        inHand = !inHand;

        if (Owner.gameObject.tag == "Player")
        {
            
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //throw the weapon away in looking direction if you are the player
            rb.velocity = Owner.GetComponent<gunCheck>().transform.up * 12;
            flytime = 0.3f;
            if (!inHand)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }


    void whoWasOwner(bool Player)
    {
        if (Player)
        {
            if (flytime <= 0)
            {
                Owner.GetComponentInChildren<gunCheck>().mygun = null;
                //stop being throwed and deattach owner
                rb.velocity = Vector2.zero;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                Owner = null;
            }
            else if (flytime > 0)
            {
                flytime -= Time.deltaTime;
                transform.Rotate(Vector3.forward * -90);
            }
        }
        else
        {
            //drop at where AI dies
            transform.position = Owner.transform.position + Owner.transform.up.normalized * 0.1f;
            Owner = null;
        }
        
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("gun touches " + collision.tag);
        if (collision.tag == "killable" && flytime > 0 && !inHand)
        {
            collision.gameObject.GetComponentInChildren<alive>().setDead(true, "gunthrow");
           
        }
    }
   




}
