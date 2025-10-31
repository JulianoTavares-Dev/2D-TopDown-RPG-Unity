using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    //Define serialized weapon info
    [SerializeField] private WeaponInfo weaponInfo;

    /// <summary>
    /// This method returns the information of the weapons.
    /// </summary>
    /// <returns></returns>
    public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }
}
