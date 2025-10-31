using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Define serialized parallax offset
    [SerializeField] private float parallaxOffset = -0.15f;

    //Define the camera that will do the parallex effect
    private Camera cam;
    //Define the starting position of the player
    private Vector2 startPos;
    //Define the travel of the player
    private Vector2 travel => (Vector2)cam.transform.position - startPos;

    /// <summary>
    /// This method runs when the game first starts running and
    /// it sets the camera for the parallex effect to be the same
    /// camera as the main camera object of the game.
    /// </summary>
    private void Awake(){
        cam = Camera.main;
    }

    /// <summary>
    /// This method runs when the game starts as well and it sets the
    /// starting position of the player to be the current position of the player.
    /// </summary>
    private void Start(){
        startPos = transform.position;
    }

    /// <summary>
    /// This method updates at fixed intervals and 
    /// it uses the starting position, the travel of the player
    /// and the parallax offset to get the position of the player.
    /// </summary>
    private void FixedUpdate(){
        transform.position = startPos + travel * parallaxOffset;
    }
}
