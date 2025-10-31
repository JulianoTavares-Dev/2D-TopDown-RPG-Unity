using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Define serialized variables related to projectile
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    //Define starting position 
    private Vector3 startPosition;

    /// <summary>
    /// This method runs when the game first starts
    /// and it sets the starting position of the player
    /// to the current position of the player.
    /// </summary>
    private void Start(){
        startPosition = transform.position;
    }

    /// <summary>
    /// This method constantly updates throughout the game
    /// and it moves the projectile and detect the distance
    /// for the enemy to shoot the bullets.
    /// </summary>
    private void Update(){
        //Reference both of these methods
        MoveProjectile();
        DetectFireDistance();
    }

    /// <summary>
    /// This method updates the range that the projectile
    /// can cause damage.
    /// </summary>
    /// <param name="projectileRange"></param>
    public void UpdateProjectileRange(float projectileRange){
        this.projectileRange = projectileRange;
    }

    /// <summary>
    /// This method updates the speed in which the projectile
    /// moves.
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void UpdateMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }

    /// <summary>
    /// This method runs when player activates the 2d collider
    /// trigger and it detects collision to apply damage if collision involves
    /// player or enemy.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other){
        //Get this game objects from unity
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        //Deals damage and destroys the projectile if it hits a player or enemy
        if(!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            } else if(!other.isTrigger && indestructible)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// This method detects the distance to fire.
    /// </summary>
    private void DetectFireDistance(){
        //Check if the player is in a distance that the enemy can reach to shoot
        if (Vector3.Distance(transform.position, startPosition) > projectileRange){
            //Destroy game object
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This method moves the projectile.
    /// </summary>
    private void MoveProjectile(){
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
