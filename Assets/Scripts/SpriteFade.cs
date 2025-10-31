using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    //Define serialized variable
    [SerializeField] private float fadeTime = .4f;

    //Define sprite renderer for the fade of sprites
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// This method shows when the game is first awake and
    /// it gets the sprite renderer component in unity.
    /// </summary>
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// This routine gradually fades out the object's sprite renderer
    /// by reducing its alpha value to zero.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SlowFadeRoutine()
    {
        //Define variable and sprite renderer start value
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        //Fades out over time
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }

        //Destroy the game object after fading out
        Destroy(gameObject);
    }
}
