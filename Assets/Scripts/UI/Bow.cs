using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    //Define serialized game objects
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    //Define attack animation
    readonly int FIRE_HASH = Animator.StringToHash("Attack");

    //Define animator for the bow attacking
    private Animator myAnimator;

    /// <summary>
    /// This method runs when the game is first awake and
    /// it initializes the animator component.
    /// </summary>
    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// This method runs when the player uses the bow to attack and
    /// it uses the attack animation and the arrow object to attack
    /// with the bow.
    /// </summary>
    public void Attack(){
        //Call on Attack animation for the bow
        myAnimator.SetTrigger(FIRE_HASH);
        //Get the arrow object from unity
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        //Define new projectile object (the arrow)
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }

    /// <summary>
    /// This method simply returns the information of the current weapon
    /// for the player.
    /// </summary>
    /// <returns></returns>
    public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }
}
