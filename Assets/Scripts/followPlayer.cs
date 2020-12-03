using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    Vector2 mouseScreenPosition;
    Vector2 newCamPos;
    [HideInInspector]
    public bool shift = false;


    void FixedUpdate()
    {
       
   
        mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (shift)
        {
            newCamPos = new Vector2(0.5f * (player.position.x + mouseScreenPosition.x), 0.5f * (player.position.y + mouseScreenPosition.y));
            shift = false;
        }
        else if (transform.position.x != player.position.x || transform.position.y != player.position.y)
        {
            newCamPos = new Vector2(transform.position.x - 0.1f * (newCamPos.x - player.position.x), transform.position.y - 0.1f * (newCamPos.y - player.position.y));
        }

            
            transform.position = new Vector3(newCamPos.x,newCamPos.y, -30);
   

    }
}
