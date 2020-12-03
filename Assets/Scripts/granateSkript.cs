using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class granateSkript : MonoBehaviour
{
    //every gun has a SpriteRenderer and an Animatior and a certain Bullet to spawn
    public SpriteRenderer mySpriteRenderer;
    public Animator myAnimator;

  

    [HideInInspector]
    public bool inHand = false;


    [HideInInspector]
    public GameObject Owner;

    float flytime = 0.0f;
    float radius = 3f;

    Rigidbody2D rb;

    public bool hasGrounded = false;
    bool flying = false;

    [SerializeField] GameObject explosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        if (inHand)
        {
            //stick to your owner
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(Owner.transform);
            transform.position = transform.parent.position;

            //make yourself unvisible
            mySpriteRenderer.enabled = false;
            myAnimator.enabled = false;



        }

        else if (!inHand && Owner != null)
        {
            transform.parent = null;
            //make yourself visible
            mySpriteRenderer.enabled = true;
            myAnimator.enabled = true;

            

        }

        if (flying && !hasGrounded)
        {
            grounded();
        }




    }


    public void switchHandState()
    {
        if (inHand) { flying = true; }

        inHand = !inHand;

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //throw the granate away in looking direction
        rb.velocity = Owner.GetComponentInChildren<gunCheck>().transform.up * 12;
         

        if (!inHand)
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        
    }


    void grounded()
    {
        if (rb.velocity.magnitude <= 0.009f)
        {

            hasGrounded = true;
            explode();

        }






    }

    void explode()
    {

        GameObject spawnedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        spawnedExplosion.GetComponent<AudioSource>().Play();
        Destroy(spawnedExplosion, 1f);
        Destroy(gameObject, 0.1f);


    }
 



}
