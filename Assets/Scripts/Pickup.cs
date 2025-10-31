using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Defines the types of pickup items available
    private enum PickUpType{
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
    }

    //Define serialized variables for the game
    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;
    [SerializeField] private int coinSoundIndex = 0;
    [SerializeField] private int staminaSoundIndex = 1;
    [SerializeField] private int healthSoundIndex = 2;

    //Define other variables for the game
    private Vector3 moveDir;
    private Rigidbody2D rb;
    private bool collected;

    //Reference to Audio Manager
    private AudioManager audioManager;
    
    /// <summary>
    /// This method runs when the game is first awake
    /// and it gets the rigidbody of the pickup items.
    /// </summary>
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        //Initializing audio manager
        audioManager = AudioManager.Instance;
    }

    /// <summary>
    /// This method runs when the game Starts and it
    /// starts the Anim Curve Spawn Point routine.
    /// </summary>
    private void Start(){
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    /// <summary>
    /// This method Updates the game consistently and 
    /// updates the pickup's movement directions and speed
    /// towards the player.
    /// </summary>
    private void Update(){
        //Get the position of the player
        Vector3 playerPos = PlayerController.Instance.transform.position;

        //If the player is close enough, the pickup item comes towards the player
        //if not, the item won't come
        if(Vector3.Distance(transform.position, playerPos) < pickUpDistance){
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        }else{
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    /// <summary>
    /// This method only updates at fixed intervals and
    /// it updates the rigidbody velocity of the pickup item.
    /// </summary>
    private void FixedUpdate(){
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// This method runs when the collider 2d is triggered and
    /// it takes care of the player picking up the item.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other){
        //If player is close enough, player picks up the item
        if (other.gameObject.GetComponent<PlayerController>()){
            //Call on the detect pickup type feature
            DetectPickupType();
            //Destory item that has been picked up
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This Routine smoothly animates the collectible's position with a
    /// curve effect.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimCurveSpawnRoutine(){
        //Get position of pickup items
        Vector2 startPoint = transform.position;
        //Make pickup items float
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);
        //Check end point of pickup items
        Vector2 endPoint = new Vector2(randomX, randomY);
        //Set time that has passed to 0
        float timePassed = 0f;
        //If time that has passed is less than pop duration
        // add tme to the time passed and make it float.
        while(timePassed < popDuration){
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    /// <summary>
    /// This method handles different pickup types by updating the
    /// player's gold amount, current health and stamina depending
    /// on what item the player picked up.
    /// </summary>
    private void DetectPickupType(){
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                GameManager.Instance.CollectCoin();
                audioManager.PlayAudioFromArray(coinSoundIndex);
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                audioManager.PlayAudioFromArray(healthSoundIndex);
                break;
            case PickUpType.StaminaGlobe:
                Stamina.Instance.RefreshStamina();
                audioManager.PlayAudioFromArray(staminaSoundIndex);
                break;
        }
    }
}
