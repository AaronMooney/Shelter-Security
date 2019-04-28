using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * TurretType script that specifys the type of turret, used by the shop.
 * */

public class TurretType : MonoBehaviour {

	public enum TurretKind { Cannon, Gatling, Missile, Punisher, Beam, AntiAir, Disruption, Shockwave };
    public TurretKind turret;
}
