using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    //Define serialized variables for the game
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private WeaponInfo weaponInfo;

    //Define other variables for the game
    private Transform weaponCollider;
    private Animator myAnimator;

    //Audio
    private AudioManager audioManager;


    private GameObject slashAnim;

    /// <summary>
    /// This method first runs when the game is first
    /// awake and it gets the animator for the sword of
    /// the player from inside unity.
    /// </summary>
    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// This method first runs when the game starts and 
    /// it assigns the weapon collider variable.
    /// </summary>
    private void Start(){
        //Set weapon collider to the weapon collider in the player controller
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        //Find the game object in unity called Slash Spawn Point
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;

        //Initialize the Audio Manager
        audioManager = AudioManager.Instance;
    }

    /// <summary>
    /// This method consistently updates the game and
    /// it calls the Mouse follow with offset method.
    /// </summary>
    private void Update(){
        MouseFollowWithOffset();
    }

    /// <summary>
    /// This method returns weapon information.
    /// </summary>
    /// <returns></returns>
    public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }

    /// <summary>
    /// This method takes care of the attack using the sword.
    /// </summary>
    public void Attack(){
        //Trigger the animator for the attack with the sword
        myAnimator.SetTrigger("Attack");
        //Set weapon collider for the sword attack
        weaponCollider.gameObject.SetActive(true);
        //Get the animations for the attack
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;

        //Play Sword attack audio
        AudioManager.Instance.PlayAudioFromArray(7);
    }

    /// <summary>
    /// This method deactivates the weapon collider when the attack
    /// animation is finished.
    /// </summary>
    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    /// <summary>
    /// This method takes care of the animation of the sword
    /// when swinging up.
    /// </summary>
    public void SwingUpFlipAnimEvent(){
        //Rotate the animation
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        //Get flipped animation sprite if the player is facing left
        if (PlayerController.Instance.FacingLeft){
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    } 

    /// <summary>
    /// This method takes care of the sword animation when
    /// swinging down.
    /// </summary>
    public void SwingDownFlipAnimEvent(){
        //Rotate the animation
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //Get flipped animation sprite if the player is facing left
        if (PlayerController.Instance.FacingLeft){
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    } 

    /// <summary>
    /// This method rotatetes the weapon of the player according to 
    /// where the mouse cursor is pointing to in the game.
    /// </summary>
    private void MouseFollowWithOffset(){
        //Check position of the mouse cursor and flip player screen accordingly
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //turn weapo to the left if the cursor in on the left
        if (mousePos.x < playerScreenPoint.x){
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } 
        //turn weapon to the right if cursor in on the right
        else{
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
