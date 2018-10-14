using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class stores methods that are called when the cooresponding main menu buttons are clicked
/// </summary>
public class PlayOnclick : MonoBehaviour
{
    private bool _AreOptionsActive = false;

    public GameObject optionsMenu;
    public string firstSceneName;

    public void whenClickedPlay()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public void whenClickedInstructions()
    {
        Debug.Log("Instructions button was clicked");
    }

    public void whenClickedCredits()
    {
        Debug.Log("Credits button was clicked");
    }

    public void whenClickedOptions()
    {
        Debug.Log("Options button was clicked");
        _AreOptionsActive = !_AreOptionsActive;
        optionsMenu.SetActive(_AreOptionsActive);
    }

    public void whenClickedQuit()
    {
        Debug.Log("Quit button was clicked");
        Application.Quit();
    }
	
}
