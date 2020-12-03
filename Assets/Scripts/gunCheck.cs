using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunCheck : MonoBehaviour
{

    [HideInInspector]
    public gunObject mygun;
    [HideInInspector]
    granateSkript myGranate;

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


   

    private void OnTriggerStay2D(Collider2D collision)
    {



        if (collision.tag == "gun" && !collision.GetComponent<gunObject>().inHand)
        {

            if (Input.GetMouseButtonDown(1))
            {
               
                mygun = collision.GetComponent<gunObject>();
                mygun.Owner = gameObject;
                mygun.switchHandState();
            }




        }
        if (collision.tag == "granate" && !collision.GetComponent<granateSkript>().inHand)
        {

            if (Input.GetMouseButtonDown(1))
            {
                myGranate = collision.GetComponent<granateSkript>();
                myGranate.Owner = gameObject;
                myGranate.switchHandState();
            }


        }
        

    }
}
