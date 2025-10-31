using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    /// <summary>
    /// This method constantly updates as  the game goes and it
    /// calls the face mouse method.
    /// </summary>
    private void Update(){
        FaceMouse();
    }

    /// <summary>
    /// This method checks the mouse position to make the mouse follow 
    /// the player.
    /// </summary>
    private void FaceMouse(){
        //Get the position of the mouse in the game and the camera
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Check the direction of the mouse
        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
