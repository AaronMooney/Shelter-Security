using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponSelection : NetworkBehaviour
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
        if (!isLocalPlayer) return;
        int previousWeapon = currentWeaponIndex;

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

        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponHolder.transform.childCount >= 2) currentWeaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponHolder.transform.childCount >= 3) currentWeaponIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && weaponHolder.transform.childCount >= 4) currentWeaponIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5) && weaponHolder.transform.childCount >= 5) currentWeaponIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6) && weaponHolder.transform.childCount >= 5) currentWeaponIndex = 5;

        if (previousWeapon != currentWeaponIndex)
        {
            SwitchWeapon();
        }
    }

    [Client]
    private void SwitchWeapon()
    {
        CmdEquip();
    }

    //So far this works only when the host switched weapon.
    [ClientRpc]
    void RpcEquip()
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
        Debug.Log("swapped " + currentWeapon.name);
    }
    

    [Command]
    public void CmdEquip()
    {
        RpcEquip();
    }
}
