using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRWeaponSelection : MonoBehaviour
{

    public int currentWeapon = 0;
    public VRTK_ControllerEvents controllerEvents;
    public GameObject controller;
    public GameObject weaponsTable;

    public GameObject[] weapons;

    void Start()
    {
        EquipWeapon();
    }

    void Update()
    {

        int previousWeapon = currentWeapon;

        if (controllerEvents.touchpadPressed && controllerEvents.GetTouchpadAxis().x > 0.7f)
        {
            Debug.Log("right");
            if (currentWeapon >= weapons.Length)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        
        if (controllerEvents.touchpadPressed && controllerEvents.GetTouchpadAxis().x < -0.7f)
        {
            Debug.Log("left");
            if (currentWeapon <= 0)
            {
                currentWeapon = weapons.Length;
            }
            else
            {
                currentWeapon--;
            }
        }

        if (previousWeapon != currentWeapon)
        {
            //EquipWeapon();
            weaponsTable.SetActive(true);
        }
    }

    void EquipWeapon()
    {
        int i = 0;
        foreach (GameObject weapon in weapons)
        {
            if (i == currentWeapon)
            {
                //weapon.SetActive(true);

                //controller.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = weapon.GetComponent<VRTK_InteractableObject>();

                //Debug.Log(controller.name);
            }
            else
            {
                //weapon.SetActive(false);
            }
            i++;
        }
    }
}
