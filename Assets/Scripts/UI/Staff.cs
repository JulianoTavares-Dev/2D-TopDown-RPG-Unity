using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    //Define game serialized variables
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    //Define animator for the staff
    private Animator myAnimator;

    //Define attack animation for the staff
    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    /// <summary>
    /// This method runs when the game is first awake and it
    /// gets the animator component from unity.
    /// </summary>
    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// This method constantly updates and it calls the 
    /// mouse follow with offset method.
    /// </summary>
    private void Update(){
        MouseFollowWithOffset();
    }

    /// <summary>
    /// This method attacks and it uses the animation for the staff attack.
    /// </summary>
    public void Attack(){
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    /// <summary>
    /// This method instantiates a new magic laser projectile at a specified spawn point and 
    /// updates its range using information from the weapon.
    /// </summary>
    public void SpawnStaffProjectileAnimEvent(){
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    /// <summary>
    /// This method returns the weapon information.
    /// </summary>
    /// <returns></returns>
    public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }

    /// <summary>
    /// This method otates the active weapon to face the mouse cursor, 
    /// applying a 180-degree rotation if the mouse is to the left of the player.
    /// </summary>
    private void MouseFollowWithOffset(){

        //Get the mouse position and the screen point of the player position
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        //Calculate the angle of rotation
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //Check if mouse is to the left or right to flip the staff accordingly.
        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else{
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
