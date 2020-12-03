using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBullet : MonoBehaviour
{
   
    public Rigidbody2D myBody;
    Vector2 mouseScreenPosition;
    Vector3 targetVelocity;
    private void Awake()
    {
        mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  
    }

    private void Update()
    {
        
        myBody.velocity += mouseScreenPosition;
    } 

}
