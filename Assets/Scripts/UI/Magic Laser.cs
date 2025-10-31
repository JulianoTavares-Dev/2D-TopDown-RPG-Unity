using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    //Define serialized variable
    [SerializeField] private float laserGrowTime = 2f;

    //Define other variables and components of the game
    private bool isGrowing = true;
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    /// <summary>
    /// This method first runs when the game is first awake and it
    /// gets the capsule collider and sprite renderer from unity.
    /// </summary>
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// This method runs when the game starts and it calls on the 
    /// Laser Face Mouse method.
    /// </summary>
    private void Start(){
        LaserFaceMouse();
    }

    /// <summary>
    /// This method checks if the indestructible objects are triggered and 
    /// if they are, it sets is Growing variable to false.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger){
            isGrowing = false;
        }
    }

    /// <summary>
    /// This method updates the range of the laser that will
    /// come out of the magic staff.
    /// </summary>
    /// <param name="laserRange"></param>
    public void UpdateLaserRange(float laserRange){
        //start laser range variables
        this.laserRange = laserRange;
        //call on the increase laser length routine
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    /// <summary>
    /// This routine gradually increases the size of the laser (length).
    /// </summary>
    /// <returns></returns>
    private IEnumerator IncreaseLaserLengthRoutine(){

        //Reset the time passed variable
        float timePassed = 0f;

        //Keep increasing the length of the laser
        while(spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            //Increase sprite renderer size
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            //Increase the offset and the size of the capsule collider for the magical laser
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, capsuleCollider2D.size.y);

            yield return null;
        }

        //Get sprite fade routine to slowly fade the magic laser from the game
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());

    }

    /// <summary>
    /// This method rotates the laser so that it points towards the mouse cursor in the game world
    /// </summary>
    private void LaserFaceMouse(){
        //Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        //Get camera mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Get direction
        Vector2 direction = transform.position - mousePosition;
        //Check to see whether direction is right or left
        transform.right = -direction;
    }
}
