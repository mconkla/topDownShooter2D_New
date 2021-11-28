using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attributes : MonoBehaviour
{
    Vector2 direction;
    public float speed = 5f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = gameObject.transform.parent.transform.up.normalized;
        rb.velocity = direction * speed;
        gameObject.transform.parent = null;


    }

    // Update is called once per frame
    void Update()
    {
        
        Ray2D ray = new Ray2D(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,0.4f);

        Debug.DrawRay(ray.origin,ray.direction  * 0.1f,Color.red);

        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject.tag == "killable")
            {
                hit.collider.gameObject.GetComponent<alive>().setDead(true,"bullet");
                Destroy(gameObject, 0f);
                return;
            }
            else if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerMovement>().alive = false;
                return;
            }
            else if (hit.collider.gameObject.tag == "wall")
            {
              
                Destroy(gameObject, 0.0f);
                return;
            }
            
            
        }
        
       


    }




}


