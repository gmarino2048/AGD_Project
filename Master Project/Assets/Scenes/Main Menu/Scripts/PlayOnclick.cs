using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores methods that are called when the cooresponding main menu buttons are clicked
/// </summary>
public class PlayOnclick : MonoBehaviour {

    bool areOptionsActive = false;
    public GameObject optionsMenu;

    public void whenClickedPlay()
    {
        Debug.Log("Playbutton was clicked");
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
        areOptionsActive = !areOptionsActive;
        optionsMenu.SetActive(areOptionsActive);

    }

    public void whenClickedQuit()
    {
        Debug.Log("Quit button was clicked");
        Application.Quit();
    }
	
}
