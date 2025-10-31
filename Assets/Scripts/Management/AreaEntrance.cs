using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
    //Set up serialized string variable transition name.
    [SerializeField] private string transitionName;

    /// <summary>
    /// This method transitions the scene, sets the camera to fllow the player,
    /// get the player position and Clear the Fade effect as the player enters
    /// the portal to go from a scene/level to the next scene/level.
    /// </summary>
    private void Start(){
        
        //Check if the transiton name is the same as the name on the scene manager
        if (transitionName == SceneManagement.Instance.SceneTransitionName){
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow(); //REMEMBER THIS IS IMPORTANT, IT MOST LIKELY is the FIX TO THE CAMERA FOLLOW error during Save and load system.
            UIFade.Instance.FadeToClear();
        }
    }
}
