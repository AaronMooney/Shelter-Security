using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * WeaponSelection script that switches between purchased weapons
 * */

public class WeaponSelection : MonoBehaviour
{
    [Header("Object fields")]
    public GameObject currentWeapon;
    public GameObject previous;
    public GameObject weaponHolder;

    private int currentWeaponIndex = 0;
    private int previousIndex;
    private string cycleKey = "Mouse ScrollWheel";

    private void Start()
    {
        WeaponSwap();
    }

    private void Update()
    {
        previousIndex = currentWeaponIndex;

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
        if (previousIndex != currentWeaponIndex)
        {
            WeaponSwap();
        }
    }

    // Switch weapons if possible
    private void WeaponSwap()
    {
        int i = 0;

        // loop through weapons and if the weapon is purchased then equip it
        foreach (Transform weapon in weaponHolder.transform)
        {
            if (i == currentWeaponIndex)
            {

                if (weapon.GetComponent<WeaponPurchased>().isPurchased)
                {
                    weapon.gameObject.SetActive(true);
                    currentWeapon = weapon.gameObject;
                } else
                {
                    // if not then change the index and re equip
                    if (previousIndex < currentWeaponIndex)
                        currentWeaponIndex++;
                    else if (previousIndex > currentWeaponIndex)
                        currentWeaponIndex--;
                    WeaponSwap();
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
