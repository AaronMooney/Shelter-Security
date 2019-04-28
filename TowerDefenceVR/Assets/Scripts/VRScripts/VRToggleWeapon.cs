using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/*
 * Aaron Mooney
 * 
 * VRToggleWeapon script that toggles the purchased state of a VR weapon.
 * */
public class VRToggleWeapon : MonoBehaviour {

    public enum GunType {Pistol, Assault, LaserRifle, Sniper, PlasmaSniper, Launcher };
    public GunType gunType;

    public bool isPurchased;

    // Sets a weapon to purchased
    public void ToggleWeapon()
    {
        isPurchased = true;
    }
}
