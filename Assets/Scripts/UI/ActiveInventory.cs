using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    //Define integer variable
    private int activeSlotIndexNum = 0;

    //Get player controls from unity
    private PlayerControls playerControls;

    /// <summary>
    /// This method runs when the game is first awake and
    /// it initializes the player controls.
    /// </summary>
    protected override void Awake(){
        //Get Awake method from the parent class
        base.Awake();

        playerControls = new PlayerControls();
    }

    /// <summary>
    /// This method runs when the game starts and it
    /// sets an event handler that calls on the Toggle Active Slot method.
    /// </summary>
    private void Start(){
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    /// <summary>
    /// This method runs when this script is enabled and 
    /// it enables the player controls.
    /// </summary>
    private void OnEnable(){
        playerControls.Enable();
    }

    /// <summary>
    /// This method calls on the toggle active highlight method
    /// to equip the starting weapon the player will have in hands.
    /// </summary>
    public void EquipStartingWeapon(){
        ToggleActiveHighLight(0);
    }

    /// <summary>
    /// This method call on the toggle active highlight method.
    /// </summary>
    /// <param name="numValue"></param>
    private void ToggleActiveSlot(int numValue){
        ToggleActiveHighLight(numValue - 1);
    }

    /// <summary>
    /// This method activates a highlight for the inventory slot at indexNum and 
    /// deactivates highlights for all other slots, then updates the active weapon
    /// </summary>
    /// <param name="indexNum"></param>
    private void ToggleActiveHighLight(int indexNum){

        //set the slot index number that is active to the index number
        activeSlotIndexNum = indexNum;

        //Foreach loop to deactivate the child game objects
        foreach(Transform inventorySlot in this.transform){
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        //Get the child of the object
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        //Call on change active weapon method
        ChangeActiveWeapon();
    }

    /// <summary>
    /// This method Changes between the different weapons available to the 
    /// player.
    /// </summary>
    private void ChangeActiveWeapon(){
        
        //If the active weapon isn't null, destroy the current weapon player has in hands
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null){
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        //Get these different components of the game
        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponent<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        //If the information of the weapon is null, 
        //return no active weapon by the player
        if (weaponInfo == null){
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        //Set other game objects and components of the game related to the inventory (weapons) of the player
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
