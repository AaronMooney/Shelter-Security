using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Aaron Mooney
 * 
 * PlayerActions script that deals with player menus and starting waves
 * */
public class PlayerActions : MonoBehaviour {

    [Header("Objects and Canvases")]
    public GameObject spawner;
    public GameObject turretShopPanel;
    public GameObject gunShopPanel;
    public GameObject menuCanvas;
    public GameObject winCanvas;

    [Header("Booleans")]
    public bool turretShopActive = false;
    public bool gunShopActive = false;
    public bool roundActive = false;
    public bool menuActive = false;
    private bool continueGame = false;

    [Header("Numerical Fields")]
    public int coinBalance = 0;
    public int round = 1;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        // Toggle the winning screen
        if (!roundActive && !continueGame && round > 30)
        {
            winCanvas.SetActive(true);
        }

        // show cursor when a menu is active
        if (turretShopActive || gunShopActive || menuActive || winCanvas.activeInHierarchy)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }

        // hit enter to start an enemy wave and increase the round number
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

        // Toggle shops
        if (Input.GetKeyDown(KeyCode.Tab) && !gunShopActive)
        {
            ToggleTurretShop();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !turretShopActive)
        {
            ToggleGunShop();
        }

        // Toggle menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
        }

        menuCanvas.SetActive(menuActive);
        spawner.SetActive(roundActive);
    }

    // Hide cursor and lock it to center of screen
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Show cursor and unlock it
    public void UnlockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
            
    }

    // Ends a round
    public void EndRound()
    {
        roundActive = false;
    }

    // Toggle visibility of turret shop
    public void ToggleTurretShop()
    {
        turretShopActive = !turretShopActive;
        turretShopPanel.SetActive(turretShopActive);
    }

    // Toggle visibility of gun shop
    public void ToggleGunShop()
    {
        gunShopActive = !gunShopActive;
        gunShopPanel.SetActive(gunShopActive);
    }

    // Add coins to balance
    public void AddCoins(int amount)
    {
        coinBalance += amount;
    }

    // Remove coins from balance
    public void RemoveCoins(int amount)
    {
        coinBalance -= amount;
    }

    // Close menu
    public void Resume()
    {
        menuActive = false;
    }

    // Continue game after winning
    public void ContinueGame()
    {
        winCanvas.SetActive(false);
        continueGame = true;
    }

    // Return to main menu
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
