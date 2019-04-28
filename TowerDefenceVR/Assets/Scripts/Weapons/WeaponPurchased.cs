using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * WeaponPurchased script that toggles the purchased state of a weapon.
 * */
public class WeaponPurchased : MonoBehaviour {

    public enum WeaponType { Pistol, AssaultRifle, LaserRifle, Sniper, PlasmaSniper, Launcher };
    public WeaponType weaponType;

    public bool isPurchased;

    // Sets a weapon to purchased
    public void PurchaseWeapon()
    {
        isPurchased = true;
    }

}
