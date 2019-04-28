using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRToggleWeapon : MonoBehaviour {

    public enum GunType {Pistol, Assault, LaserRifle, Sniper, PlasmaSniper, Launcher };
    public GunType gunType;

    public bool isPurchased;

    public void ToggleWeapon()
    {
        isPurchased = true;
    }



}
