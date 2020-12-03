using UnityEngine;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
  

    public Rigidbody2D m_Rigidbody2D;

    private Vector3 m_Velocity = Vector3.zero;

    [HideInInspector]
    public Vector2 direction;


    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    
    private void FixedUpdate()
    {
        if (FindObjectOfType<PlayerMovement>().alive)
        {
            lookAtMouse();
        }
        

    }


    public void Move(float moveHoriz, float moveVertic)
    {
            // Move the character by finding the target velocity
            Vector2 targetVelocity = new Vector2(moveHoriz, moveVertic);
         
            m_Rigidbody2D.velocity = targetVelocity;
      



    }

    private void lookAtMouse()
    {
        
        // convert mouse position into world coordinates
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at
       direction = (mouseScreenPosition - (Vector2)m_Rigidbody2D.transform.position).normalized;
            // set vector of transform directly
        m_Rigidbody2D.transform.up = direction;
    }

}
