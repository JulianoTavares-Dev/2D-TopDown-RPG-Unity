using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    //Set up serialized variables and others
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private bool stagger;
    [SerializeField] private bool oscillate;

    //Set is shooting enemy bool variable to false
    private bool isShooting = false;

    /// <summary>
    /// This method method in Unity is a special method that gets called when the script is loaded or a value in the inspector is changed. 
    /// It is typically used to ensure that serialized field values are within acceptable ranges or to enforce specific relationships between variables. 
    /// This method is particularly useful for maintaining consistency and avoiding invalid values being set in the Unity Editor.
    /// </summary>
    private void OnValidate(){

        //Ensure all serialized fields maintain valid and consistent values
        if (oscillate) { stagger = true; }
        if (!oscillate){ stagger = false; }
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1){ burstCount = 1; }
        if (timeBetweenBursts < 0.1f){ timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f){ restTime = 0.1f; }
        if (startingDistance < 0.1f){ startingDistance = 0.1f; }
        if (angleSpread == 0){ projectilesPerBurst = 1;}
        if (bulletMoveSpeed <= 0){ bulletMoveSpeed = 0.1f; }
    }

    /// <summary>
    /// This method Attacks if the condition is shooting is true and 
    /// it starts the coroutine for the shoot routine.
    /// </summary>
    public void Attack(){

        //Checks if is shooting is true
        if (!isShooting){

            //If condition is met, start coroutine and call on the method below
            StartCoroutine(ShootRoutine());
        }
    }

    /// <summary>
    /// This routine shoots projectiles in bursts, with options for staggering shots
    /// oscillating angles, and resting between bursts.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootRoutine(){

        //Set shooting to true meaning player is shooting
        isShooting = true;

        //Initialize other float variables
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        //Call on the Target Cone Of Influence method
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        //Calculates time interval between projectiles in a burst
        if(stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

        //For loop while burst count is small
        for (int i = 0; i < burstCount; i++){
            
            //check if oscillate is flase and call the target cone of influence method
            if (!oscillate){
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            //check if oscillate is true and call the target cone of influence method
            if (oscillate && i % 2 != 1){
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            } 
            
            //check if oscillate is true and set angles
            else if (oscillate){
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }
            
            //Another for loop while projectiles per burst are small
            for (int j = 0; j < projectilesPerBurst; j++){
                
                //Set vector 2 position of the bullet spawn
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                //Set bullet game object
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                //check if bullet component is active
                if (newBullet.TryGetComponent(out Projectile projectile)){
                    
                    //Update the speed in which the projectile (bullet) is moving
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

                //pause coroutine if stagger is true
                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            //pause for a few seconds if stagger is false
            if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
        }

        //pause for a few seconds and set shooting to false
        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    /// <summary>
    /// This method calculates the angles for projecting the projectiles
    /// (bullets) when shooting.
    /// </summary>
    /// <param name="startAngle">angle the projectile starts</param>
    /// <param name="currentAngle">current angle of the projectile</param>
    /// <param name="angleStep">angle step of the projectile</param>
    /// <param name="endAngle">angle the projectile ends at</param>
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        //Set different angles for the projectile shoot
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0){

            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    /// <summary>
    /// This method calculates and returns the position that a bullet
    /// will be spawned according to the angle.
    /// </summary>
    /// <param name="currentAngle"></param>
    /// <returns></returns>
    private Vector2 FindBulletSpawnPos(float currentAngle){

        //Set x and y position for the bullet spawn
        float x  = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        //Set gector 2 position x and y
        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
