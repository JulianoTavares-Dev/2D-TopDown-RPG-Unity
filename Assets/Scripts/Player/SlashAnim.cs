using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    //Define particle system for the game
    private ParticleSystem ps;

    /// <summary>
    /// This method first runs when the game is first
    /// awake and it gets the particle system component.
    /// </summary>
    private void Awake(){
        ps = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// This method consistently updates the game,
    /// checking if the particle system is working.
    /// </summary>
    private void Update(){
        //Check if condition is met
        if(ps && !ps.IsAlive()){
            //Destroy object (reference to method)
            DestroySelf();
        }
    }

    /// <summary>
    /// This method destroys the object.
    /// </summary>
    public void DestroySelf(){
        Destroy(gameObject);
    }
}
