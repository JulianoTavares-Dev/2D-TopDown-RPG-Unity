using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    //Defines attribute in Unity allows you to specify a slider in the Inspector for a float variable, restricting its value between 0 and 1
    [Range(0,1)]

    //Define serialized variables related to the transparency of objects
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = .4f;

    //Define sprite renderer and the tilemap that has been used for the game
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    /// <summary>
    /// This method runs when the game is first awake and it
    /// gets the sprite renderer and tilemap used in the
    /// game.
    /// </summary>
    private void Awake(){
        //Get sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Get tilemap used for the game
        tilemap = GetComponent<Tilemap>();
    }

    /// <summary>
    /// This method is triggered when the player collides with the 2d object
    /// and it triggers the fade effect.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other){
        //Check if player object has collided
        if (other.gameObject.GetComponent<PlayerController>()){
            if (spriteRenderer) {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            } else if (tilemap){
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    /// <summary>
    /// This method is triggered when the player exits the area that
    /// triggers the fade effect.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.GetComponent<PlayerController>()){
            
            if (spriteRenderer) {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            } else if (tilemap){
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }

    /// <summary>
    /// This routine smoothly fades the alpha value of the sprite renderer.
    /// </summary>
    /// <param name="spriteRenderer">get the spriterender of the object</param>
    /// <param name="fadeTime">define an amount of time for it to fade</param>
    /// <param name="startValue">define a starting value for the fade</param>
    /// <param name="targetTransparency">define the expected traparency</param>
    /// <returns></returns>
    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency){

        //Define elapsed time
        float elapsedTime = 0;

        //While loop contines to go and it fades the sprite
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null; 
        }
    }

    /// <summary>
    /// This routine smoothly fades the alpha value of the tilemap.
    /// </summary>
    /// <param name="tilemap">get the tilemap of the game</param>
    /// <param name="fadeTime">define the amount of time for it to fade</param>
    /// <param name="startValue">define a starting value for the fade</param>
    /// <param name="targetTransparency">define the expected transparency</param>
    /// <returns></returns>
    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency){

        //Define elapsed time
        float elapsedTime = 0;

        //While loop contines to go and it fades the specific tiles of the map
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null; 
        }
    }
}
