using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    //Defines the source of the camera shake feature 
    private CinemachineImpulseSource source;

    /// <summary>
    /// This method runs when the game is first awake, it
    /// calls the awake method from the parent class and it
    /// gets the Cinemachine shake source and set it to the variable
    /// source.
    /// </summary>
    protected override void Awake(){
        //Call awake method from parent class
        base.Awake();
        //Set source variable to the component in the game that 
        //takes care of the shake
        source = GetComponent<CinemachineImpulseSource>();
    }

    /// <summary>
    /// This method Shakes the Screen by
    /// using the source variable and generating
    /// and impulse (shake) in the scene the player
    /// is exploring.
    /// </summary>
    public void ShakeScreen(){
        source.GenerateImpulse();
    }
}
