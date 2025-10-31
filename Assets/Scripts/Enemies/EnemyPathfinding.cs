using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    //Setting up serialized variable
    [SerializeField] private float moveSpeed = 2f;

    //Setting up rigidbody 2d, vector 2 for moveDir,
    //knockback and sprite renderer.
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// This method is called when the game first start and it
    /// gets component for sprite renderer, knockback and rigidbody2d.
    /// </summary>
    private void Awake(){

        //Get component Sprite Renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Get component Knockback
        knockback = GetComponent<Knockback>();

        //Get component Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This method is called at a fixed time interval and is used for physics-related updates.
    /// It checks if the enemy is getting knockedback, apply rigidbody physics to it and,
    /// update the sprite of the player to make it face the direction depending on the condition being met.
    /// </summary>
    private void FixedUpdate(){

        //Checks if the enemy got knockedback
        if (knockback.GettingKnockedBack) { return ; }
        
        //If condition is true, physics affect the rigidbody2d component of the enemy
        //making the enemy move backwards.
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        //Update the sprite's flipX property based on the movement direction.
        //If the enemy is moving to the left (negative x direction), flip the sprite.
        if (moveDir.x < 0){
            spriteRenderer.flipX = true;
        } 
        //Otherwise if the enemy is moving to the right (positive x direction), don't flip the sprite.
        else if (moveDir.x > 0){
            spriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// This method makes the enemy move. 
    /// </summary>
    /// <param name="targetPosition"></param>
    public void MoveTo(Vector2 targetPosition){
        moveDir = targetPosition;
    }

    /// <summary>
    /// This method makes the enemy stop moving.
    /// </summary>
    public void StopMoving(){
        moveDir = Vector3.zero;
    }
}
