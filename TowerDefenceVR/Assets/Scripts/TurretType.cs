using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretType : MonoBehaviour {

	public enum TurretKind { Cannon, Gatling, Missile, Punisher, Beam, AntiAir, Disruption, Shockwave };
    public TurretKind turret;
}
