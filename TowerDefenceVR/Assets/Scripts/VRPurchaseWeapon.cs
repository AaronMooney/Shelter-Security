using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class VRPurchaseWeapon : MonoBehaviour
{

    private VRTK_Button_UnityEvents buttonEvents;
    [SerializeField] private GameObject weapon;
    [SerializeField] private VRShop shop;
    [SerializeField] private ShopCosts costs;
    [SerializeField] private VRTK_ObjectTooltip tooltip;

    //public enum GunType { Assault, LaserRifle, Sniper, PlasmaSniper, Launcher };
    //public GunType gunType;

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

    private void HandlePush(object sender, Control3DEventArgs e)
    {
        VRTK_Logger.Info("Pushed");


        PurchaseWeapon();
    }

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

    private void Toggle(int cost)
    {
        shop.coinBalance -= cost;
        Destroy(gameObject.transform.parent.gameObject);
        //SetPurchased();
        gun.isPurchased = true;
    }

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

    //private void SetPurchased()
    //{
    //    if (gun.gunType == VRToggleWeapon.GunType.Assault)
    //    {
    //        gun.isPurchased = true;
    //    }
    //    if (gun.gunType == VRToggleWeapon.GunType.LaserRifle)
    //    {
    //        laserRiflePurchased = true;
    //    }
    //    if (gun.gunType == VRToggleWeapon.GunType.Sniper)
    //    {
    //        sniperPurchased = true;
    //    }
    //    if (gun.gunType == VRToggleWeapon.GunType.PlasmaSniper)
    //    {
    //        plasmaSniperPurchased = true;
    //    }
    //    if (gun.gunType == VRToggleWeapon.GunType.Launcher)
    //    {
    //        launcherPurchased = true;
    //    }
    //}


}
