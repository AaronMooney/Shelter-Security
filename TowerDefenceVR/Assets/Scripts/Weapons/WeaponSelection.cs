using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    private int previousIndex;
    private string cycleKey = "Mouse ScrollWheel";
    public GameObject currentWeapon;
    public GameObject previous;
    public GameObject weaponHolder;

    void Start()
    {
        WeaponSwap();
    }

    void Update()
    {
        previousIndex = currentWeaponIndex;
        //Debug.Log(currentWeaponIndex + " " + currentWeapon.name);

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

        if (previousIndex != currentWeaponIndex)
        {
            WeaponSwap();
        }
    }


    private void WeaponSwap()
    {
        int i = 0;
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
