using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Aaron Mooney
 * 
 * SceneController script that toggles VR and loads other scenes.
 * */

public class SceneController : MonoBehaviour {

    [SerializeField] private Toggle VRToggle;

    // Toggles VR depending on checkbox value in main menu
    public void ToggleVR()
    {
        VRConfig.VREnabled = VRToggle.isOn;
    }

    // Loads the game
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // Exits the application
    public void QuitGame()
    {
        Application.Quit();
    }

    // Loads multiplayer scene
    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("NetworkingPrototype");
    }

    // Loads the controls scene
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    // Load the Menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
