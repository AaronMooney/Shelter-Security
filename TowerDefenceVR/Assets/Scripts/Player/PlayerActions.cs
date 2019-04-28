using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;
    public GameObject turretShopPanel;
    public GameObject gunShopPanel;
    public bool turretShopActive = false;
    public bool gunShopActive = false;
    public GameObject menuCanvas;
    public GameObject winCanvas;
    public bool menuActive;
    private bool continueGame;

    public int coinBalance = 0;

    public int round = 1;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {

        if (!roundActive && !continueGame && round > 30)
        {
            winCanvas.SetActive(true);
        }

        if (turretShopActive || gunShopActive || menuActive || winCanvas.activeInHierarchy)
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
        }

        menuCanvas.SetActive(menuActive);



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

    public void Resume()
    {
        menuActive = false;
    }

    public void ContinueGame()
    {
        winCanvas.SetActive(false);
        continueGame = true;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
