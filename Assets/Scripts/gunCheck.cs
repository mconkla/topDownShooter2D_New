using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunCheck : MonoBehaviour
{

    [HideInInspector]
    public gunObject mygun;
    [HideInInspector]
    granateSkript myGranate;


    public gunObject gunOnFloor;
    public granateSkript granateOnFloor;

    // Update is called once per frame
    void Update()
    {

        if(gameObject.transform.childCount > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if(mygun == gameObject.transform.GetChild(0).GetComponent<gunObject>() && myGranate == null)
                mygun.switchHandState();
                else if (myGranate != null)
                {
                    myGranate.switchHandState();
                }

                else
                {
                    gameObject.transform.GetChild(0).GetComponent<gunObject>().switchHandState();
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "gun" && !collision.GetComponent<gunObject>().inHand)
        {
            Debug.Log("Player enters: Gun");
            this.gunOnFloor = collision.gameObject.GetComponent<gunObject>();
        }
        if (collision.tag == "granate" && !collision.GetComponent<granateSkript>().inHand)
        {
            Debug.Log("Player enters: Granade");
            this.granateOnFloor = collision.gameObject.GetComponent<granateSkript>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "gun" && !collision.GetComponent<gunObject>().inHand)
        {
            Debug.Log("Player exits: Gun");
            this.gunOnFloor = null;
        }
        if (collision.tag == "granate" && !collision.GetComponent<granateSkript>().inHand)
        {
            Debug.Log("Player exits: Granade");
            this.granateOnFloor = null;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "gun" && !this.gunOnFloor.inHand)
        {
            Debug.Log("Player staying in: Gun");
            if (Input.GetMouseButton(1))
            {
                mygun = this.gunOnFloor;
                mygun.Owner = gameObject;
                mygun.switchHandState();
            }
        }
        if (collision.tag == "granate" && !this.granateOnFloor.inHand)
        {
            Debug.Log("Player staying in: Granade");
            if (Input.GetMouseButton(1))
            {
                myGranate = this.granateOnFloor;
                myGranate.Owner = gameObject;
                myGranate.switchHandState();
            }
        }
    }
}
