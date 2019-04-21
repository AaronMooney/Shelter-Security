using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;
    public GameObject turretShopPanel;
    public GameObject gunShopPanel;
    public bool turretShopActive = false;
    public bool gunShopActive = false;

    public int coinBalance = 0;

    public int round = 1;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {

        
        if (turretShopActive || gunShopActive)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(roundActive);
            if (!roundActive)
            {
                roundActive = !roundActive;
                spawner.GetComponent<SpawnWave>().SetRoundLimit(round);
                round++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !gunShopActive)
        {
            ToggleTurretShop();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !turretShopActive)
        {
            ToggleGunShop();
        }



        spawner.SetActive(roundActive);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
            
    }

    public void EndRound()
    {
        roundActive = false;
    }

    public void ToggleTurretShop()
    {
        turretShopActive = !turretShopActive;
        turretShopPanel.SetActive(turretShopActive);
    }

    public void ToggleGunShop()
    {
        gunShopActive = !gunShopActive;
        gunShopPanel.SetActive(gunShopActive);
    }

    public void AddCoins(int amount)
    {
        coinBalance += amount;
    }

    public void RemoveCoins(int amount)
    {
        coinBalance -= amount;
    }
}
