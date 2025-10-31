using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    //Defining the side the player is facing
    public bool FacingLeft { get { return facingLeft; } }

    //Defining the serialized fields
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;

    //Defining player controls
    private PlayerControls playerControls;
    //Defining player movement
    private Vector2 movement;
    //Defining the player's rigid body
    private Rigidbody2D rb;
    //Defining the animator for the player
    private Animator myAnimator;
    //Defining the sprite renderer for the player
    private SpriteRenderer mySpriteRender;
    //Defining the knockback on the player
    private Knockback knockback;
    //Defining how fast the player moves
    private float startingMoveSpeed;
    //Defining boolean variable player is facing the left side
    private bool facingLeft = false;
    //Defining boolean variable player is dashing
    private bool isDashing = false;

    /// <summary>
    /// This method runs when the game is first awake and it starts all the 
    /// components above.
    /// </summary>
    protected override void Awake(){
        //Call awake method from parent class
        base.Awake();

        //Start all the variables defined above
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    /// <summary>
    /// This method runs when the game starts and it takes care of
    /// checking what weapon the player is holding,
    /// whether the player is dashing and the speed in which the player
    /// is moving.
    /// </summary>
    private void Start(){
        //Check if player is dashing
        playerControls.Combat.Dash.performed +=_=> Dash();

        //Setting how fast the player moves
        startingMoveSpeed = moveSpeed;

        //Setting weapon player is using
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    // <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        playerControls.Enable();
    }

    /// <summary>
    /// This method is called when the object disables and is inactive.
    /// </summary>
    private void OnDisable(){
        playerControls.Disable();
    }

    /// <summary>
    /// This method is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        //Reference to player input method
        PlayerInput();

        //Get the position of the player
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);
    }

    /// <summary>
    /// This method is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        //Set the side the player is facing
        //references facing direcetion method
        AdjustPlayerFacingDirection();
        //references move method
        Move();
    }

    /// <summary>
    /// This method returns the collider
    /// of the player's weapons.
    /// </summary>
    /// <returns></returns>
    public Transform GetWeaponCollider(){
        return weaponCollider;
    }

    /// <summary>
    /// This method gets the input from the player 
    /// for movement.
    /// </summary>
    private void PlayerInput()
    {
        //reads player's movement as vector 2
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        //Updates the animator of the player according to the movement
        //either in the X axis or the Y axis
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    /// <summary>
    /// This method actually moves the player.
    /// </summary>
    private void Move()
    {
        //Check if player is alive and isn't getting knocked back
        //by an enemy
        if (knockback.GettingKnockedBack || PlayerHealth.Instance.isDead) { return; }
        
        //rigidbody (player) moves
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// This method Adjusts in which direction (right or left)
    /// the player is facing according to where the pointer
    /// of the mouse is pointing.
    /// </summary>
    private void AdjustPlayerFacingDirection()
    {
        //Get vector 3 mouse position and screen point in which
        // the player is in (using the main camera)
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //Check if mouse cursor is to the left or to 
        //the right and flips the sprite to make player
        //face the right or the left
        if (mousePos.x < playerScreenPoint.x){
            mySpriteRender.flipX = true;
            facingLeft = true;
        } else {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    /// <summary>
    /// This method takes care of the dashing movement
    /// of the player.
    /// </summary>
    private void Dash(){
        //Checks if player isn't already dashing and player has stamina available
        if (!isDashing && Stamina.Instance.CurrentStamina > 0) {

            //If condition above is met, lowers the stamina, the player
            //dashes, move speed increases and leaves trail behind
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    /// <summary>
    /// This method is a coroutine that handles the end of a dash.
    /// <returns></returns>
    private IEnumerator EndDashRoutine(){

        //Define values for float variables
        float dashTime = .2f;
        float dashCD = .25f;
        //Pause for the time of the dash
        yield return new WaitForSeconds(dashTime);
        //set the move speed of the player to the 
        //moving speed the player started with
        moveSpeed = startingMoveSpeed;
        //Stop the trail emission
        myTrailRenderer.emitting = false;
        //Pause for the time of the dash cd
        yield return new WaitForSeconds(dashCD);
        //set dashing back to false
        isDashing = false;
    }
}
