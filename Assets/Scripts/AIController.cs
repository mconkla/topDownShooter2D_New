using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Transform player;
    gunObject gunOfPlayer;
    granateSkript granadeOfPlayer;
    public GameObject lastPlayerShoot;

    [HideInInspector]
    public bool iSawYou,iHearYou = false;
   

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;

    }
    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponentInChildren<alive>().dead && player.GetComponent<PlayerMovement>().alive)
        {

            checkSight();
            checkGranade();

        }

    }
    private void FixedUpdate()
    {
        gunOfPlayer = FindObjectOfType<PlayerMovement>().mygun;
        granadeOfPlayer = FindObjectOfType<PlayerMovement>().myGranate;
        
    }

    void checkSight()
    {
        

        Vector2 playerPos = (Vector2)player.position - (Vector2)transform.position;
        Vector2 lookingDirection = (Vector2)transform.up;

        Ray2D ray = new Ray2D(transform.position, playerPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, playerPos.magnitude);
        
        Debug.DrawRay(ray.origin, ray.direction, Color.black);
     
        float angle = Vector2.Angle(playerPos, lookingDirection);
 

        if (iSawYou)
        {

            transform.up = playerPos;
            gameObject.GetComponent<EnemyAI>().enabled = true;
            gameObject.GetComponentInChildren<alive>().shooting();

        }
        if (iHearYou)
        {
            
            gameObject.GetComponent<EnemyAI>().enabled = true;
        }
        if(!iSawYou && !iHearYou)
        {
            gameObject.GetComponent<EnemyAI>().target = null;
            gameObject.GetComponent<EnemyAI>().enabled = false;
            //gameObject.GetComponentInChildren<alive>().child.GetChild(0).gameObject.SetActive(false);
        }

        if (angle <= 90f && playerPos.magnitude < 3f && hit.collider != null && hit.collider.tag == "Player")
        {
            iSawYou = true;
            iHearYou = false;
            

        }



        if (playerPos.magnitude < 4f && gunOfPlayer != null && gunOfPlayer.spawnedBullet != null)
        {

            lastPlayerShoot.transform.position = player.position;
            
            iHearYou = true;
        }


      



    }

    void checkGranade()
    {
        
        if (granadeOfPlayer != null && granadeOfPlayer.hasGrounded)
        {

            if (Vector2.Distance(transform.position, granadeOfPlayer.transform.position) <= 0.6f)
            {
                GetComponent<CircleCollider2D>().isTrigger = true;
                gameObject.GetComponentInChildren<alive>().dead = true;
            }
            else
            {
                lastPlayerShoot.transform.position = granadeOfPlayer.transform.position;
                iHearYou = true;
            }
            
        }
    }

 

}
