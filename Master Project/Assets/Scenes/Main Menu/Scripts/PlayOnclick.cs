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
    private bool _AreCreditsActive = false;
    private bool _AreInstructionsActive = false;

    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject instructionsMenu;
    public string firstSceneName;

    public void whenClickedPlay()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public void whenClickedInstructions()
    {
        Debug.Log("Instructions button was clicked");
        _AreInstructionsActive = !_AreInstructionsActive;
        instructionsMenu.SetActive(_AreInstructionsActive);
    }

    public void whenClickedCredits()
    {
        Debug.Log("Credits button was clicked");
        _AreCreditsActive = !_AreCreditsActive;
        creditsMenu.SetActive(_AreCreditsActive);
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
    
    public void whenOptionsClickedBack()
    {
        _AreOptionsActive = false;
        optionsMenu.SetActive(_AreOptionsActive);
    }

    public void whenCreditsClickedBack()
    {
        _AreCreditsActive = false;
        creditsMenu.SetActive(_AreCreditsActive);
    }

    public void whenInstructionsClickedBack()
    {
        _AreInstructionsActive = false;
        instructionsMenu.SetActive(_AreInstructionsActive);
    }

}
