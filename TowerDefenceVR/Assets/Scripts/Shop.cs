using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * Shop script for the non-VR game allowing players to purchase weapons and turrets.
 * 
 * */

public class Shop : MonoBehaviour {

    // Turret objects to be placed
    [Header("Turrets")]
    public GameObject cannon;
    public GameObject gatling;
    public GameObject missileLauncher;
    public GameObject punisher;
    public GameObject antiAir;
    public GameObject beamCannon;
    public GameObject shockwave;
    public GameObject disruptor;

    // Turret mesh that is carried until placed so that turrets do not shoot while carrying
    [Header("Turret Meshes")]
    public GameObject cannonMesh;
    public GameObject gatlingMesh;
    public GameObject missileLauncherMesh;
    public GameObject punisherMesh;
    public GameObject antiAirMesh;
    public GameObject beamCannonMesh;
    public GameObject shockwaveMesh;
    public GameObject disruptorMesh;

    [Header("Other")]
    public GameObject player;
    public ShopCosts costs;

    private int coinBalance;

    // Update is called once per frame
    private void Update()
    {
        coinBalance = player.GetComponent<PlayerActions>().coinBalance;
    }

    // Purchase a turret and carry it in front of the player 
    public void PickupTurret(GameObject mesh, GameObject turret)
    {
        player.GetComponent<PickupAndPlace>().Pickup(mesh, turret);
        player.GetComponent<PlayerActions>().ToggleTurretShop();
    }

    // Purchase a weapon
    public void PurchaseWeapon(WeaponPurchased.WeaponType type)
    {
        // loop through each weapon available and compare its type and set weapon as purchased if the type matches
        foreach (Transform weapon in player.GetComponent<WeaponSelection>().weaponHolder.transform)
        {
            if (weapon.GetComponent<WeaponPurchased>().weaponType == type)
            {
                weapon.GetComponent<WeaponPurchased>().PurchaseWeapon();
            }
        }
        player.GetComponent<PlayerActions>().ToggleGunShop();
    }

    // Purchase Cannon
    public void PurchaseCannon()
    {
        if (coinBalance >= costs.cannonCost)
        {
            PickupTurret(cannonMesh, cannon);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.cannonCost);
        }
    }

    // Purchase Gatling Gun
    public void PurchaseGatling()
    {
        if (coinBalance >= costs.gatlingCost)
        {
            PickupTurret(gatlingMesh, gatling);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.gatlingCost);
        }
    }

    // Purchase Missile Launcher
    public void PurchaseMissile()
    {
        if (coinBalance >= costs.missileLauncherCost)
        {
            PickupTurret(missileLauncherMesh, missileLauncher);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.missileLauncherCost);
        }
    }

    // Purchase Punisher
    public void PurchasePunisher()
    {
        if (coinBalance >= costs.punisherCost)
        {
            PickupTurret(punisherMesh, punisher);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.punisherCost);
        }
    }

    // Purchase Beam Cannon
    public void PurchaseBeamCannon()
    {
        if (coinBalance >= costs.beamCannonCost)
        {
            PickupTurret(beamCannonMesh, beamCannon);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.beamCannonCost);
        }
    }

    // Purchase Anti Air Turret
    public void PurchaseAntiAir()
    {
        if (coinBalance >= costs.antiAirCost)
        {
            PickupTurret(antiAirMesh, antiAir);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.antiAirCost);
        }
    }

    // Purchase Shockwave Turret
    public void PurchaseShockwave()
    {
        if (coinBalance >= costs.shockwaveCost)
        {
            PickupTurret(shockwaveMesh, shockwave);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.shockwaveCost);
        }
    }

    // Purchase Disruptor Turret
    public void PurchaseDisruptor()
    {
        if (coinBalance >= costs.disruptorCost)
        {
            PickupTurret(disruptorMesh, disruptor);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.disruptorCost);
        }
    }

    // Purchase Assault Rifle
    public void PurchaseAssaultRifle()
    {
        if (coinBalance >= costs.assaultRifleCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.AssaultRifle);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.assaultRifleCost);
        }
    }

    // Purchase Laser Rifle
    public void PurchaseLaserRifle()
    {
        if (coinBalance >= costs.laserRifleCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.LaserRifle);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.laserRifleCost);
        }
    }

    // Purchase Sniper Rifle
    public void PurchaseSniper()
    {
        if (coinBalance >= costs.sniperCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.Sniper);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.sniperCost);
        }
    }

    // Purchase Plasma Sniper Rifle
    public void PurchasePlasmaSniper()
    {
        if (coinBalance >= costs.plasmaSniperCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.PlasmaSniper);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.plasmaSniperCost);
        }
    }

    // Purchase Rocket Launcher
    public void PurchaseLauncher()
    {
        if (coinBalance >= costs.launcherCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.Launcher);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.launcherCost);
        }
    }
}
