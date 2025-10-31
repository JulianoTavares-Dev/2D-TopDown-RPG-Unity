using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    //Set up animator for Idle animation of enemy
    private Animator myAnimator;

    /// <summary>
    /// This method is called when the game first start and it
    /// gets the component for the animator for the enemy.
    /// </summary>
    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// This method is called when the game first starts just like
    /// the Awake method but it is called on the frame when
    /// a script is enabled and it checks if animator is null or not set
    /// and retrieves the current state of the animator on layer 0 and plays the animation again.
    /// </summary>
    private void Start(){

        //checks if the animator is null or not set
        if (!myAnimator) { return; }

        //retrieves the current state of the animator on layer 0
        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);

        //Plays the current animation again
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
