using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    [SerializeField] private Toggle VRToggle;

    public void ToggleVR()
    {
        VRConfig.VREnabled = VRToggle.isOn;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("NetworkingPrototype");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
}
