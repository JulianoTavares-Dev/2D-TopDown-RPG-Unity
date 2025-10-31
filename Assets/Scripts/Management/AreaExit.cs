using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    //Set up serialized scene that will load and transition name of the scene
    //Scene that will load
    [SerializeField] private string sceneToLoad;
    //Transition name
    [SerializeField] private string sceneTransitionName;

    //Set up load waiting time
    private float waitToLoadTime = 1f;

    /// <summary>
    /// This method makes the player transition from this scene to the next scene
    /// as the player touches the collider 2d which is a trigger for the player to
    /// exit the scene the player is on and go to the entrance of the next scene.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other){

        //Check if the player has touched the Collider 2d and triggered the exit from that scene/level
        if (other.gameObject.GetComponent<PlayerController>()){

            //If condition is met, transition the scene, 
            //clear the fade effect and load screen routine
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadScreenRoutine());
        }
    }

    /// <summary>
    /// This method Loads the Screen routine by waiting for the time to load
    /// and if the time to load is equals or more than 0, it returns null.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadScreenRoutine(){
        while (waitToLoadTime >= 0){
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        //Load the next scene/level that should be loaded
        SceneManager.LoadScene(sceneToLoad);
    }
}
