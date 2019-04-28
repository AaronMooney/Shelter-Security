using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameObject player;

    public GameObject cannon;
    public GameObject gatling;
    public GameObject missileLauncher;
    public GameObject punisher;
    public GameObject antiAir;
    public GameObject beamCannon;
    public GameObject shockwave;
    public GameObject disruptor;

    public GameObject cannonMesh;
    public GameObject gatlingMesh;
    public GameObject missileLauncherMesh;
    public GameObject punisherMesh;
    public GameObject antiAirMesh;
    public GameObject beamCannonMesh;
    public GameObject shockwaveMesh;
    public GameObject disruptorMesh;

    private int coinBalance;
    public ShopCosts costs;


    private void Update()
    {
        coinBalance = player.GetComponent<PlayerActions>().coinBalance;
    }

    public void PickupTurret(GameObject mesh, GameObject turret)
    {
        player.GetComponent<PickupAndPlace>().Pickup(mesh, turret);
        player.GetComponent<PlayerActions>().ToggleTurretShop();
    }

    public void PurchaseWeapon(WeaponPurchased.WeaponType type)
    {
        foreach (Transform weapon in player.GetComponent<WeaponSelection>().weaponHolder.transform)
        {
            if (weapon.GetComponent<WeaponPurchased>().weaponType == type)
            {
                weapon.GetComponent<WeaponPurchased>().PurchaseWeapon();
            }
        }
        player.GetComponent<PlayerActions>().ToggleGunShop();
    }

    public void PurchaseCannon()
    {
        if (coinBalance >= costs.cannonCost)
        {
            PickupTurret(cannonMesh, cannon);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.cannonCost);
        }
    }

    public void PurchaseGatling()
    {
        if (coinBalance >= costs.gatlingCost)
        {
            PickupTurret(gatlingMesh, gatling);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.gatlingCost);
        }
    }

    public void PurchaseMissile()
    {
        if (coinBalance >= costs.missileLauncherCost)
        {
            PickupTurret(missileLauncherMesh, missileLauncher);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.missileLauncherCost);
        }
    }

    public void PurchasePunisher()
    {
        if (coinBalance >= costs.punisherCost)
        {
            PickupTurret(punisherMesh, punisher);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.punisherCost);
        }
    }

    public void PurchaseBeamCannon()
    {
        if (coinBalance >= costs.beamCannonCost)
        {
            PickupTurret(beamCannonMesh, beamCannon);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.beamCannonCost);
        }
    }

    public void PurchaseAntiAir()
    {
        if (coinBalance >= costs.antiAirCost)
        {
            PickupTurret(antiAirMesh, antiAir);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.antiAirCost);
        }
    }

    public void PurchaseShockwave()
    {
        if (coinBalance >= costs.shockwaveCost)
        {
            PickupTurret(shockwaveMesh, shockwave);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.shockwaveCost);
        }
    }

    public void PurchaseDisruptor()
    {
        if (coinBalance >= costs.disruptorCost)
        {
            PickupTurret(disruptorMesh, disruptor);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.disruptorCost);
        }
    }

    public void PurchaseAssaultRifle()
    {
        if (coinBalance >= costs.assaultRifleCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.AssaultRifle);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.assaultRifleCost);
        }
    }

    public void PurchaseLaserRifle()
    {
        if (coinBalance >= costs.laserRifleCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.LaserRifle);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.laserRifleCost);
        }
    }

    public void PurchaseSniper()
    {
        if (coinBalance >= costs.sniperCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.Sniper);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.sniperCost);
        }
    }

    public void PurchasePlasmaSniper()
    {
        if (coinBalance >= costs.plasmaSniperCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.PlasmaSniper);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.plasmaSniperCost);
        }
    }

    public void PurchaseLauncher()
    {
        if (coinBalance >= costs.launcherCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.Launcher);
            player.GetComponent<PlayerActions>().RemoveCoins(costs.launcherCost);
        }
    }
}
