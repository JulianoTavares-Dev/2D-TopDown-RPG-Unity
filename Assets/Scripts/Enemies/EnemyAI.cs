using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    //Set up Serialized variables
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    //Set up Attacking variable.
    private bool canAttack = true;

    /// <summary>
    /// This method sets Roaming and Attacking States to be used later on.
    /// </summary>
    private enum State{
        Roaming,
        Attacking
    }

    //Set up Vector 2 and time variables.
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    //Set up state and enemypathfinding variables.
    private State state;
    private EnemyPathfinding enemyPathfinding;

    /// <summary>
    /// This method is called when the game first start and it
    /// Gets the component for enemyPathfinding ans sets the state
    /// to Roaming.
    /// </summary>
    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    /// <summary>
    /// This method is called when the game first starts just like
    /// the Awake method but it is called on the frame when
    /// a script is enabled and it gets the roamPosition.
    /// </summary>
    private void Start(){
        roamPosition = GetRoamingPosition();
    }

    /// <summary>
    /// This method initializes the MovementStateControl
    /// throughout the game runs as the Update method
    /// keeps updating.
    /// </summary>
    private void Update(){
        MovementStateControl();
    }

    /// <summary>
    /// This method manages the state of your enemy's behavior. 
    /// It switches between different states (Roaming and Attacking) and 
    /// calls corresponding methods (Roaming() and Attacking()) based on the current state.
    /// </summary>
    private void MovementStateControl(){
        //Change State from
        switch (state)
        {
            //Default state - Roaming
            default:
            case State.Roaming:
                Roaming();
            break;

            //New state - Attacking
            case State.Attacking:
                Attacking();
            break;
        }
    }

    /// <summary>
    /// This method controls how the enemy behaves while it is in the roaming state, 
    /// managing movement towards a designated roam position, checking proximity to 
    /// the player for potential attack, and periodically updating the roam position
    /// </summary>
    private void Roaming(){

        //Update the Roaming time with the delta Time
        timeRoaming += Time.deltaTime;

        //Move the enemy using pathfinding to the designated roamPosition
        enemyPathfinding.MoveTo(roamPosition);

        //Check if the Distance of the enemy is less than the attack range of the player 
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange){
            
            //Switch player's state to Attacking so player will attack
            state = State.Attacking;
        }

        //Change the roamPosition after exceeding the roamChangeDirFloat duration.
        if (timeRoaming > roamChangeDirFloat){
            roamPosition = GetRoamingPosition();
        }

    }

    public void Attack()
    {
        Debug.Log("Enemy attacks");
    }

    /// <summary>
    /// This method controls how the player will go from attacking mode to roaming mode
    /// depending on whether the condition has been met, condition being if the enemy
    /// is too far from the player for the player to attack.
    /// </summary>
    private void Attacking(){

        //Checks if the Distance of the enemy is bigger than the attack range
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange){

            //Switches from Attacking to roaming mode meaning player won't attack
            state = State.Roaming;
        }

        //Checks if the attack range isn't equal to 0 and player can attack
        if (attackRange != 0 && canAttack){
            
            //if condition is met player can attack is false
            canAttack = false;
            
            //Perform the attack action by casting enemyType to IEnemy and calling the Attack method.
            (enemyType as IEnemy).Attack();

            //check if the enemy should stop moving while attacking.
            if (stopMovingWhileAttacking){
                //Stop enemy movement during attack
                enemyPathfinding.StopMoving();
            } else {
                //continue moving to the roam position while attacking
                enemyPathfinding.MoveTo(roamPosition);
            }

            //Start coroutine to handle the cool down between each attack
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    /// <summary>
    /// This method handles the routine for the attack cool down
    /// called on the method above.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCooldownRoutine(){

        //Wait for seconds which is the attack cool down time
        yield return new WaitForSeconds(attackCooldown);

        //After waiting for the attack cool down time, player can attack again
        canAttack = true;
    }

    /// <summary>
    /// This method resets the roaming timer and generates a new random roaming position for the enemy.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetRoamingPosition(){

        //reset time roaming count to 0
        timeRoaming = 0f;

        //Generate a new Vector2 with random x and y values between -1 and 1, normalize it to ensure the vector has a magnitude of 1.
        // This gives the enemy a new direction to roam in.
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
