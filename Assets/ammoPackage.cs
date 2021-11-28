using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPackage : MonoBehaviour
{
    public int ammoAmount = 100;
    public string ammoType = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().mygun.inHand)
        {
            Debug.Log("fill that magg");

            int startMag = collision.gameObject.GetComponent<PlayerMovement>().mygun.startMag;
            int currentMag = collision.gameObject.GetComponent<PlayerMovement>().mygun.magazin;

            int difference = startMag - currentMag;
            if (difference >= ammoAmount)
                difference = ammoAmount;
            

            collision.gameObject.GetComponent<PlayerMovement>().mygun.magazin += difference;
            ammoAmount -= difference;

            if(ammoAmount <= 0)
               Destroy(this.gameObject);

        }
    }
}
