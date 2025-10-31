using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    //Set up serialized visual effect for the destruction of items in 
    //the game
    [SerializeField] private GameObject destroyVFX;

    /// <summary>
    /// This method triggers item drop in the case of destruction
    /// of the destructible objects.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other){

        //drops pickup items if destructible object is destroyed by the player
        if(other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>()){
            GetComponent<PickupSpawner>().DropItems();
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
