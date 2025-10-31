using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    //Set up serialized white flash material
    [SerializeField] private Material whiteFlashMat;

    //Set up serailized restore default time
    [SerializeField] private float restoreDefaultMatTime = .2f;

    //Set up the default material
    private Material defaultMat;
    //Set up sprite renderer for the flash
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// This method runs when the game first awakes and it
    /// gets the component for the sprite renderer of the flash
    /// and sets the default material to the sprite renderer
    /// material.
    /// </summary>
    private void Awake(){
        
        //Get the sprite renderer object
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Set the default material to the same material of the recently
        //initialized sprite renderer material object
        defaultMat = spriteRenderer.material;
    }

    /// <summary>
    /// This method returns the time for the default material.
    /// </summary>
    /// <returns></returns>
    public float GetRestoreMatTime(){
        return restoreDefaultMatTime;
    }

    /// <summary>
    /// This routine temporarily changes the object's material
    /// to the flash material.
    /// </summary>
    /// <returns></returns>
    public IEnumerator FlashRoutine(){

        //Set sprie render material to white flash material
        spriteRenderer.material = whiteFlashMat;
        //Pause for a few seconds
        yield return new WaitForSeconds(restoreDefaultMatTime);
        //Set sprite render material back to default material
        spriteRenderer.material = defaultMat;
    }
}
