using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    Vector2 mouseScreenPosition;


    // Update is called once per frame
    void FixedUpdate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mouseScreenPosition.x, mouseScreenPosition.y);

    }
}
