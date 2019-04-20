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


    public void PickupTurret(GameObject mesh, GameObject turret)
    {
        player.GetComponent<PickupAndPlace>().Pickup(mesh, turret);
        player.GetComponent<PlayerActions>().ToggleShop();
    }

    public void PurchaseCannon()
    {
        PickupTurret(cannonMesh, cannon);
    }

    public void PurchaseGatling()
    {
        PickupTurret(gatlingMesh, gatling);
    }

    public void PurchaseMissile()
    {
        PickupTurret(missileLauncherMesh, missileLauncher);
    }

    public void PurchasePunisher()
    {
        PickupTurret(punisherMesh, punisher);
    }

    public void PurchaseBeamCannon()
    {
        PickupTurret(beamCannonMesh, beamCannon);
    }

    public void PurchaseAntiAir()
    {
        PickupTurret(antiAirMesh, antiAir);
    }

    public void PurchaseShockwave()
    {
        PickupTurret(shockwaveMesh, shockwave);
    }

    public void PurchaseDisruptor()
    {
        PickupTurret(disruptorMesh, disruptor);
    }
}
