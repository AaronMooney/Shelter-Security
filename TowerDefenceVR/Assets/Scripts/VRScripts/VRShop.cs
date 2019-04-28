using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VRTK;

/*
 * Aaron Mooney
 * 
 * VRShop script handles all turret shop actions in VR
 * */
public class VRShop : MonoBehaviour
{
    [Header("Controller")]
    public GameObject rightController;

    [Header("Turret Meshes")]
    [SerializeField] private GameObject cannonMesh;
    [SerializeField] private GameObject gatlingMesh;
    [SerializeField] private GameObject missileLauncherMesh;
    [SerializeField] private GameObject punisherMesh;
    [SerializeField] private GameObject antiAirMesh;
    [SerializeField] private GameObject beamCannonMesh;
    [SerializeField] private GameObject shockwaveMesh;
    [SerializeField] private GameObject disruptionMesh;
    [SerializeField] private GameObject selectedTurret;

    [Header("Turret Objects")]
    public GameObject cannon;
    public GameObject gatling;
    public GameObject missileLauncher;
    public GameObject punisher;
    public GameObject antiAir;
    public GameObject beamCannon;
    public GameObject shockwave;
    public GameObject disruptor;

    [Header("Turret Images")]
    [SerializeField] private Sprite canonImage;
    [SerializeField] private Sprite gatlingImage;
    [SerializeField] private Sprite missileImage;
    [SerializeField] private Sprite punisherImage;
    [SerializeField] private Sprite antiAirImage;
    [SerializeField] private Sprite beamImage;
    [SerializeField] private Sprite shockwaveImage;
    [SerializeField] private Sprite disruptorImage;

    [Header("Other")]
    public VRTK_RadialMenu menu;
    public GameObject invisibleCursor;
    public int coinBalance;

    private bool canPlace = false;
    private GameObject surfacePlotInstance;
    private VRTK_Pointer pointer;
    private ShopCosts costs;

    private bool isCanonPurchased = false;
    private bool isGatlingPurchased = false;
    private bool isMissilePurchased = false;
    private bool isPunisherPurchased = false;
    private bool isAntiAirPurchased = false;
    private bool isBeamPurchased = false;
    private bool isShockwavePurchased = false;
    private bool isDisruptorPurchased = false;

    // Use this for initialization
    private void Start()
    {
        pointer = rightController.GetComponent<VRTK_Pointer>();
        costs = GetComponent<ShopCosts>();
    }

    // Set the images for the radial menu on the right controller
    public void SetMenuItems()
    {
        menu.buttons[0].ButtonIcon = canonImage;
        menu.RegenerateButtons();

        menu.buttons[1].ButtonIcon = gatlingImage;
        menu.RegenerateButtons();

        menu.buttons[2].ButtonIcon = punisherImage;
        menu.RegenerateButtons();

        menu.buttons[3].ButtonIcon = missileImage;
        menu.RegenerateButtons();

        menu.buttons[4].ButtonIcon = antiAirImage;
        menu.RegenerateButtons();

        menu.buttons[5].ButtonIcon = beamImage;
        menu.RegenerateButtons();

        menu.buttons[6].ButtonIcon = shockwaveImage;
        menu.RegenerateButtons();

        menu.buttons[7].ButtonIcon = disruptorImage;
        menu.RegenerateButtons();
    }

    // Change the pointer of the controller to render the selected turret mesh
    private void TogglePointer(GameObject g)
    {
        pointer.enabled = false;
        pointer.pointerRenderer.enabled = false;
        rightController.GetComponent<VRTK_BezierPointerRenderer>().customCursor = g;
        pointer.enabled = true;
        pointer.pointerRenderer.enabled = true;
    }

    // Reset the pointer to default
    private void ResetPointer()
    {
        pointer.enabled = false;
        pointer.pointerRenderer.enabled = false;
        rightController.GetComponent<VRTK_BezierPointerRenderer>().customCursor = invisibleCursor;
        pointer.enabled = true;
        pointer.pointerRenderer.enabled = true;
    }

    // Purchase cannon and set the pointer to cannon if the player can purchase
    public void SetCursorCanon()
    {
        if (coinBalance >= costs.cannonCost)
        {
            TogglePointer(cannonMesh);
            selectedTurret = cannon;
            isCanonPurchased = true;
        } else
        {
            ResetPointer();
        }
    }

    // Purchase gatling gun and set the pointer to gatling gun if the player can purchase
    public void SetCursorGatling()
    {
        if (coinBalance >= costs.gatlingCost)
        {
            TogglePointer(gatlingMesh);
            selectedTurret = gatling;
            isGatlingPurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Purchase beam cannon and set the pointer to beam cannon if the player can purchase
    public void SetCursorBeamCanon()
    {
        if (coinBalance >= costs.beamCannonCost)
        {
            TogglePointer(beamCannonMesh);
            selectedTurret = beamCannon;
            isBeamPurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Purchase missile launcher and set the pointer to missile launcher if the player can purchase
    public void SetCursorMissile()
    {
        if (coinBalance >= costs.missileLauncherCost)
        {
            TogglePointer(missileLauncherMesh);
            selectedTurret = missileLauncher;
            isMissilePurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Purchase anti air turret and set the pointer to anti air turret if the player can purchase
    public void SetCursorAntiAir()
    {
        if (coinBalance >= costs.antiAirCost)
        {
            TogglePointer(antiAirMesh);
            selectedTurret = antiAir;
            isAntiAirPurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Purchase punisher and set the pointer to punisher if the player can purchase
    public void SetCursorPunisher()
    {
        if (coinBalance >= costs.punisherCost)
        {
            TogglePointer(punisherMesh);
            selectedTurret = punisher;
            isPunisherPurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Purchase shockwave turrey and set the pointer to shockwave turret if the player can purchase
    public void SetCursorShockwave()
    {
        if (coinBalance >= costs.shockwaveCost)
        {
            TogglePointer(shockwaveMesh);
            selectedTurret = shockwave;
            isShockwavePurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Purchase disruptor and set the pointer to disruptor if the player can purchase
    public void SetCursorDisrupt()
    {
        if (coinBalance >= costs.disruptorCost)
        {
            TogglePointer(disruptionMesh);
            selectedTurret = disruptor;
            isDisruptorPurchased = true;
        }
        else
        {
            ResetPointer();
        }
    }

    // Check if a turret is purchased
    private bool IsPurchased(GameObject g)
    {
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Cannon)
        {
            return isCanonPurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Gatling)
        {
            return isGatlingPurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Missile)
        {
            return isMissilePurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Beam)
        {
            return isBeamPurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Punisher)
        {
            return isPunisherPurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.AntiAir)
        {
            return isAntiAirPurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Shockwave)
        {
            return isShockwavePurchased;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Disruption)
        {
            return isDisruptorPurchased;
        }
        return false;
    }

    // Purchase turret
    private void Purchase(GameObject g)
    {
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Cannon)
        {
            coinBalance -= costs.cannonCost;
            isCanonPurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Gatling)
        {
            coinBalance -= costs.gatlingCost;
            isGatlingPurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Missile)
        {
            coinBalance -= costs.missileLauncherCost;
            isMissilePurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Beam)
        {
            coinBalance -= costs.beamCannonCost;
            isBeamPurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Punisher)
        {
            coinBalance -= costs.punisherCost;
            isPunisherPurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.AntiAir)
        {
            coinBalance -= costs.antiAirCost;
            isAntiAirPurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Shockwave)
        {
            coinBalance -= costs.shockwaveCost;
            isShockwavePurchased = false;
        }
        if (g.GetComponent<TurretType>().turret == TurretType.TurretKind.Disruption)
        {
            coinBalance -= costs.disruptorCost;
            isDisruptorPurchased = false;
        }
    }

    // Instantiate turret on a valid position
    public void SpawnTurret()
    {
        if (selectedTurret != null)
        {
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            GameObject[] antiairs = GameObject.FindGameObjectsWithTag("AntiAir");

            GameObject[] objs = turrets.Concat(antiairs).ToArray();

            // Create new object at pointer position
            surfacePlotInstance = new GameObject();
            surfacePlotInstance.transform.position = pointer.pointerRenderer.GetDestinationHit().point;

            // if no turrets exist then position is valid
            if (objs.Length == 0) canPlace = true;

            // loop through each turret and if the distance between them and the pointer position is greater than 10 then the position is valid
            foreach (GameObject g in objs)
            {
                if (Vector3.Distance(surfacePlotInstance.transform.position, g.transform.position) > 10)
                    canPlace = true;
                else
                    canPlace = false;
            }

            // Spawn the turret
            if (canPlace && IsPurchased(selectedTurret))
            {
                Vector3 newPos = new Vector3(surfacePlotInstance.transform.position.x, 0, surfacePlotInstance.transform.position.z);
                Instantiate(selectedTurret, newPos, Quaternion.identity);
                Purchase(selectedTurret);
                //UpdateEnemyPaths();
            }
            else
            {
                Debug.Log("Turret too close");
            }
            Destroy(surfacePlotInstance);
        }
    }

    // Add coins to balance
    public void AddCoins(int amount)
    {
        coinBalance += amount;
    }

    // Method that updated enemy paths, currently unused due to a bug with some enemies freezing.
    private void UpdateEnemyPaths()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 || GameObject.FindGameObjectsWithTag("Aerial").Length > 0)
        {
            GameObject[] groundUnits = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] drones = GameObject.FindGameObjectsWithTag("Aerial");

            GameObject[] enemies = groundUnits.Concat(drones).ToArray();

            // loop through all enemies and update their path
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyAI>().UpdatePath();
            }
        }
    }
}
