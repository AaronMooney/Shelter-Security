using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * ShopCosts script that contains the cost of each turret and weapon for the shop to use.
 * */

public class ShopCosts : MonoBehaviour
{
    [Header("Turrets")]
    public int cannonCost = 60;
    public int gatlingCost = 85;
    public int missileLauncherCost = 100;
    public int beamCannonCost = 100;
    public int punisherCost = 110;
    public int antiAirCost = 120;
    public int shockwaveCost = 50;
    public int disruptorCost = 40;

    [Header("Weapons")]
    public int assaultRifleCost = 120;
    public int laserRifleCost = 250;
    public int sniperCost = 150;
    public int plasmaSniperCost = 270;
    public int launcherCost = 300;
}
