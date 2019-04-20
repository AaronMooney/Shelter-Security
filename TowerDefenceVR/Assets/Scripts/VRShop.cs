using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VRTK;

public class VRShop : MonoBehaviour {

    public GameObject rightController;
    public GameObject cannonMesh;
    public GameObject gatlingMesh;
    public GameObject missileLauncherMesh;
    public GameObject punisherMesh;
    public GameObject antiAirMesh;
    public GameObject beamCannonMesh;
    public GameObject shockwaveMesh;
    public GameObject disruptionMesh;
    public GameObject selectedTurret;

    public GameObject cannon;
    public GameObject gatling;
    public GameObject missileLauncher;
    public GameObject punisher;
    public GameObject antiAir;
    public GameObject beamCannon;
    public GameObject shockwave;
    public GameObject disruptor;

    private bool isCanonPurchased = true;
    private bool isGatlingPurchased = true;
    private bool isMissilePurchased = true;
    private bool isPunisherPurchased = true;
    private bool isAntiAirPurchased = true;
    private bool isBeamPurchased = true;
    private bool isShockwavePurchased = true;
    private bool isDisruptorPurchased = true;

    public Sprite canonImage;
    public Sprite gatlingImage;
    public Sprite missileImage;
    public Sprite punisherImage;
    public Sprite antiAirImage;
    public Sprite beamImage;
    public Sprite shockwaveImage;
    public Sprite disruptorImage;

    public VRTK_RadialMenu menu;
    private bool canPlace = false;

    private GameObject surfacePlotInstance;
    private VRTK_Pointer pointer;

    // Use this for initialization
    void Start () {
        pointer = rightController.GetComponent<VRTK_Pointer>();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void isPurchased()
    {
        if (isCanonPurchased)
        {
            menu.buttons[0].ButtonIcon = canonImage;
            menu.RegenerateButtons();
        }
        if (isGatlingPurchased)
        {
            menu.buttons[1].ButtonIcon = gatlingImage;
            menu.RegenerateButtons();
        }
        if (isPunisherPurchased)
        {
            menu.buttons[2].ButtonIcon = punisherImage;
            menu.RegenerateButtons();
        }
        if (isMissilePurchased)
        {
            menu.buttons[3].ButtonIcon = missileImage;
            menu.RegenerateButtons();
        }
        if (isAntiAirPurchased)
        {
            menu.buttons[4].ButtonIcon = antiAirImage;
            menu.RegenerateButtons();
        }
        if (isBeamPurchased)
        {
            menu.buttons[5].ButtonIcon = beamImage;
            menu.RegenerateButtons();
        }
        if (isShockwavePurchased)
        {
            menu.buttons[6].ButtonIcon = shockwaveImage;
            menu.RegenerateButtons();
        }
        if (isDisruptorPurchased)
        {
            menu.buttons[7].ButtonIcon = disruptorImage;
            menu.RegenerateButtons();
        }
    }

    private void TogglePointer(GameObject g)
    {
        pointer.enabled = false;
        pointer.pointerRenderer.enabled = false;
        rightController.GetComponent<VRTK_BezierPointerRenderer>().customCursor = g;
        pointer.enabled = true;
        pointer.pointerRenderer.enabled = true;
    }

    public void SetCursorCanon()
    {
        if (isCanonPurchased)
        {
            TogglePointer(cannonMesh);
            selectedTurret = cannon;
        }
    }

    public void SetCursorGatling()
    {
        if (isGatlingPurchased)
        {
            TogglePointer(gatlingMesh);
            selectedTurret = gatling;
        }
    }

    public void SetCursorBeamCanon()
    {
        if (isBeamPurchased)
        {
            TogglePointer(beamCannonMesh);
            selectedTurret = beamCannon;
        }
    }

    public void SetCursorMissile()
    {
        if (isMissilePurchased)
        {
            TogglePointer(missileLauncherMesh);
            selectedTurret = missileLauncher;
        }
    }

    public void SetCursorAntiAir()
    {
        if (isAntiAirPurchased)
        {
            TogglePointer(antiAirMesh);
            selectedTurret = antiAir;
        }
    }

    public void SetCursorPunisher()
    {
        if (isPunisherPurchased)
        {
            TogglePointer(punisherMesh);
            selectedTurret = punisher;
        }
    }

    public void SetCursorShockwave()
    {
        if (isShockwavePurchased)
        {
            TogglePointer(shockwaveMesh);
            selectedTurret = shockwave;
        }
    }

    public void SetCursorDisrupt()
    {
        if (isDisruptorPurchased)
        {
            TogglePointer(disruptionMesh);
            selectedTurret = disruptor;
        }
    }

    public void SpawnTurret()
    {
        if (selectedTurret != null)
        {
            Debug.Log("Selected Turret" + selectedTurret.name);
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            GameObject[] antiairs = GameObject.FindGameObjectsWithTag("AntiAir");

            GameObject[] objs = turrets.Concat(antiairs).ToArray();
            surfacePlotInstance = new GameObject();
            surfacePlotInstance.transform.position = pointer.pointerRenderer.GetDestinationHit().point;

            foreach (GameObject g in objs)
            {
                Debug.Log(g.name);
                if (Vector3.Distance(surfacePlotInstance.transform.position, g.transform.position) > 10)
                    canPlace = true;
                else
                    canPlace = false;
            }

            if (canPlace)
            {
                Vector3 newPos = new Vector3(surfacePlotInstance.transform.position.x, 0, surfacePlotInstance.transform.position.z);
                Instantiate(selectedTurret, newPos, Quaternion.identity);
                UpdateEnemyPaths();
            } else
            {
                Debug.Log("Turret too close");
            }
            Destroy(surfacePlotInstance);
        }
    }

    private void UpdateEnemyPaths()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 || GameObject.FindGameObjectsWithTag("Aerial").Length > 0){
            GameObject[] groundUnits = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] drones = GameObject.FindGameObjectsWithTag("Aerial");

            GameObject[] enemies = groundUnits.Concat(drones).ToArray();

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyAI>().UpdatePath();
            }
        }
    }
}
