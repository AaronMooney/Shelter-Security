using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Aaron Mooney
 * 
 * WeaponSelectionMP script that switches between purchased weapons for multiplayer
 * */
public class WeaponSelectionMP : NetworkBehaviour
{
    [SyncVar]
    public int currentWeaponIndex = 0;
    private string cycleKey = "Mouse ScrollWheel";
    public GameObject currentWeapon;
    public GameObject previous;
    public GameObject weaponHolder;

    void Start()
    {
        if (!isLocalPlayer) return;
        SwitchWeapon();
    }

    void Update()
    {
        // if it is not the local player then return
        if (!isLocalPlayer) return;
        int previousWeapon = currentWeaponIndex;

        // Change the weapon index on scrollwheel scroll
        if (Input.GetAxis(cycleKey) < 0.0f)
        {
            if (currentWeaponIndex >= weaponHolder.transform.childCount - 1)
            {
                currentWeaponIndex = 0;
            }
            else
            {
                currentWeaponIndex++;
            }
        }

        if (Input.GetAxis(cycleKey) > 0.0f)
        {
            if (currentWeaponIndex <= 0)
            {
                currentWeaponIndex = weaponHolder.transform.childCount - 1;
            }
            else
            {
                currentWeaponIndex--;
            }
        }

        // Change current weapon with numpad
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponHolder.transform.childCount >= 2) currentWeaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponHolder.transform.childCount >= 3) currentWeaponIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && weaponHolder.transform.childCount >= 4) currentWeaponIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5) && weaponHolder.transform.childCount >= 5) currentWeaponIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6) && weaponHolder.transform.childCount >= 5) currentWeaponIndex = 5;

        // if the previous index and current are different then switch weapons
        if (previousWeapon != currentWeaponIndex)
        {
            SwitchWeapon();
        }
    }

    // Client method to switch weapons that calls a command
    [Client]
    private void SwitchWeapon()
    {
        WeaponSwap();
        CmdEquip();
    }

    // Replicate the equip to all clients
    // Client switches weapon but model stays as pistol.
    [ClientRpc]
    void RpcEquip()
    {
        if (isLocalPlayer) return;
        WeaponSwap();
    }

    // Switch weapon
    private void WeaponSwap()
    {
        int i = 0;
        foreach (Transform weapon in weaponHolder.transform)
        {
            if (i == currentWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.gameObject;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    // Command method that calls the replicate method
    [Command]
    public void CmdEquip()
    {
        RpcEquip();
    }
}
