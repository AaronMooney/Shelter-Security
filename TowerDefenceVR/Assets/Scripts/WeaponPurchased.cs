using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPurchased : MonoBehaviour {

    public enum WeaponType { Pistol, AssaultRifle, LaserRifle, Sniper, PlasmaSniper, Launcher };
    public WeaponType weaponType;

    public bool isPurchased;

    public void PurchaseWeapon()
    {
        isPurchased = true;
    }

}
