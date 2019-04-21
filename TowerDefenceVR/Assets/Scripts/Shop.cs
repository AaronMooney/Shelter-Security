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
    private int cannonCost = 60;
    private int gatlingCost = 85;
    private int missileLauncherCost = 100;
    private int beamCannonCost = 100;
    private int punisherCost = 110;
    private int antiAirCost = 120;
    private int shockwaveCost = 50;
    private int disruptorCost = 40;

    private int assaultRifleCost = 120;
    private int laserRifleCost = 250;
    private int sniperCost = 150;
    private int plasmaSniperCost = 270;
    private int launcherCost = 300;


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
        if (coinBalance >= cannonCost)
        {
            PickupTurret(cannonMesh, cannon);
            player.GetComponent<PlayerActions>().RemoveCoins(cannonCost);
        }
    }

    public void PurchaseGatling()
    {
        if (coinBalance >= gatlingCost)
        {
            PickupTurret(gatlingMesh, gatling);
            player.GetComponent<PlayerActions>().RemoveCoins(gatlingCost);
        }
    }

    public void PurchaseMissile()
    {
        if (coinBalance >= missileLauncherCost)
        {
            PickupTurret(missileLauncherMesh, missileLauncher);
            player.GetComponent<PlayerActions>().RemoveCoins(missileLauncherCost);
        }
    }

    public void PurchasePunisher()
    {
        if (coinBalance >= punisherCost)
        {
            PickupTurret(punisherMesh, punisher);
            player.GetComponent<PlayerActions>().RemoveCoins(punisherCost);
        }
    }

    public void PurchaseBeamCannon()
    {
        if (coinBalance >= beamCannonCost)
        {
            PickupTurret(beamCannonMesh, beamCannon);
            player.GetComponent<PlayerActions>().RemoveCoins(beamCannonCost);
        }
    }

    public void PurchaseAntiAir()
    {
        if (coinBalance >= antiAirCost)
        {
            PickupTurret(antiAirMesh, antiAir);
            player.GetComponent<PlayerActions>().RemoveCoins(antiAirCost);
        }
    }

    public void PurchaseShockwave()
    {
        if (coinBalance >= shockwaveCost)
        {
            PickupTurret(shockwaveMesh, shockwave);
            player.GetComponent<PlayerActions>().RemoveCoins(shockwaveCost);
        }
    }

    public void PurchaseDisruptor()
    {
        if (coinBalance >= disruptorCost)
        {
            PickupTurret(disruptorMesh, disruptor);
            player.GetComponent<PlayerActions>().RemoveCoins(disruptorCost);
        }
    }

    public void PurchaseAssaultRifle()
    {
        if (coinBalance >= assaultRifleCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.AssaultRifle);
            player.GetComponent<PlayerActions>().RemoveCoins(assaultRifleCost);
        }
    }

    public void PurchaseLaserRifle()
    {
        if (coinBalance >= laserRifleCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.LaserRifle);
            player.GetComponent<PlayerActions>().RemoveCoins(laserRifleCost);
        }
    }

    public void PurchaseSniper()
    {
        if (coinBalance >= sniperCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.Sniper);
            player.GetComponent<PlayerActions>().RemoveCoins(sniperCost);
        }
    }

    public void PurchasePlasmaSniper()
    {
        if (coinBalance >= plasmaSniperCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.PlasmaSniper);
            player.GetComponent<PlayerActions>().RemoveCoins(plasmaSniperCost);
        }
    }

    public void PurchaseLauncher()
    {
        if (coinBalance >= launcherCost)
        {
            PurchaseWeapon(WeaponPurchased.WeaponType.Launcher);
            player.GetComponent<PlayerActions>().RemoveCoins(launcherCost);
        }
    }
}
