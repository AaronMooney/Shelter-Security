using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

/*
 * Aaron Mooney
 * 
 * VRPurchaseWeapon script that sets a weapon as purchased when its respective button is pushed
 * */
public class VRPurchaseWeapon : MonoBehaviour
{

    private VRTK_Button_UnityEvents buttonEvents;

    [Header("Script References")]
    [SerializeField] private GameObject weapon;
    [SerializeField] private VRShop shop;
    [SerializeField] private ShopCosts costs;
    [SerializeField] private VRTK_ObjectTooltip tooltip;


    private VRToggleWeapon gun;

    private bool assaultPurchased;
    private bool laserRiflePurchased;
    private bool sniperPurchased;
    private bool plasmaSniperPurchased;
    private bool launcherPurchased;

    private void Start()
    {
        gun = weapon.GetComponent<VRToggleWeapon>();
        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
        {
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
        }
        buttonEvents.OnPushed.AddListener(HandlePush);

        UpdateTooltip();
    }

    // This method is called when a button is pushed
    private void HandlePush(object sender, Control3DEventArgs e)
    {
        PurchaseWeapon();
    }

    // Check the weapon type and compare cost with coin balance and purchase weapon if possible
    private void PurchaseWeapon()
    {
        if (gun.gunType == VRToggleWeapon.GunType.Assault)
        {
            if (shop.coinBalance >= costs.assaultRifleCost)
            {
                Toggle(costs.assaultRifleCost);
            }
        }
        if (gun.gunType == VRToggleWeapon.GunType.LaserRifle)
        {
            if (shop.coinBalance >= costs.laserRifleCost)
            {
                Toggle(costs.laserRifleCost);
            }
        }
        if (gun.gunType == VRToggleWeapon.GunType.Sniper)
        {
            if (shop.coinBalance >= costs.sniperCost)
            {
                Toggle(costs.sniperCost);
            }
        }
        if (gun.gunType == VRToggleWeapon.GunType.PlasmaSniper)
        {
            if (shop.coinBalance >= costs.plasmaSniperCost)
            {
                Toggle(costs.plasmaSniperCost);
            }
        }
        if (gun.gunType == VRToggleWeapon.GunType.Launcher)
        {
            if (shop.coinBalance >= costs.launcherCost)
            {
                Toggle(costs.launcherCost);
            }
        }
    }

    // Purchases a weapon and destroys a button and its tooltip
    private void Toggle(int cost)
    {
        shop.coinBalance -= cost;
        Destroy(gameObject.transform.parent.gameObject);
        gun.isPurchased = true;
    }


    // Update the tooltip of each button with the cost of each weapon
    private void UpdateTooltip()
    {
        string text = "";

        if (gun.gunType == VRToggleWeapon.GunType.Assault)
        {
            text = "Assault Rifle - " + costs.assaultRifleCost;
        }
        if (gun.gunType == VRToggleWeapon.GunType.LaserRifle)
        {
            text = "Laser Rifle - " + costs.laserRifleCost;
        }
        if (gun.gunType == VRToggleWeapon.GunType.Sniper)
        {
            text = "Sniper Rifle - " + costs.sniperCost;
        }
        if (gun.gunType == VRToggleWeapon.GunType.PlasmaSniper)
        {
            text = "Plasma Rifle - " + costs.plasmaSniperCost;
        }
        if (gun.gunType == VRToggleWeapon.GunType.Launcher)
        {
            text = "  Launcher - " + costs.launcherCost;
        }

        tooltip.displayText = text;
    }
}
