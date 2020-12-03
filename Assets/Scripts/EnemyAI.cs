using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed;
    public float nextWaypointDistance = 0.3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Rigidbody2D rb;
    Seeker seeker;

  
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<AIController>().iSawYou)
        {
            target = FindObjectOfType<PlayerMovement>().transform;
            
        }
        if (gameObject.GetComponent<AIController>().iHearYou)
        {
            
            target = gameObject.GetComponent<AIController>().lastPlayerShoot.transform;
            
        }

        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();


        
            InvokeRepeating("UpdatePath", 0f, 0.1f);
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void Update()
    {
   
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!GetComponentInChildren<alive>().dead)
        {
            followPath();

        }
        else
        {
            rb.simulated = false;

        }

        if (gameObject.GetComponent<AIController>().iSawYou)
        {
            target = FindObjectOfType<PlayerMovement>().transform;

        }
        if (gameObject.GetComponent<AIController>().iHearYou)
        {

            target = gameObject.GetComponent<AIController>().lastPlayerShoot.transform;

        }
        if (path == null)
            return;

        
       
        
    }
    

    void followPath()
    {
        if (path != null)
        {
            if (currentWaypoint < path.vectorPath.Count)
            {
                reachedEndOfPath = false;
            }
        

       
            if (Vector2.Distance(transform.position,target.position) > 0.4f)
            {
            transform.position = Vector2.MoveTowards(transform.position,path.vectorPath[currentWaypoint], speed);
            
            //rb.AddForceAtPosition(force,transform.position,ForceMode2D.Impulse);
            GetComponentInChildren<alive>().child.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                GetComponentInChildren<alive>().child.GetChild(0).gameObject.SetActive(false);
                reachedEndOfPath = true;
                return;
            }

        



        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        
        if (distance < nextWaypointDistance)
            {
            if(currentWaypoint +1 < path.vectorPath.Count)
            transform.up = path.vectorPath[currentWaypoint+1] - transform.position;
            currentWaypoint++;
            }

        }
    }

   
}
