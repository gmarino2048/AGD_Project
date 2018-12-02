using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class stores methods that are called when the cooresponding main menu buttons are clicked
/// </summary>
public class PlayOnclick : MonoBehaviour
{
    private bool _AreOptionsActive = false;
    private bool _AreCreditsActive = false;
    private bool _AreInstructions1Active = false;
    private bool _AreInstructions2Active = false;

    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject instructionsMenu;
    public GameObject instructions2Menu;
    public Slider textSpeedSlider;
    

    public string firstSceneName;

    public void whenClickedPlay()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public void whenClickedInstructions()
    {
        Debug.Log("Instructions button was clicked");
        _AreInstructions1Active = !_AreInstructions1Active;
        instructionsMenu.SetActive(_AreInstructions1Active);
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

    public void whenInstructions1ClickedBack()
    {
        _AreInstructions1Active = false;
        _AreInstructions2Active = true;
        instructionsMenu.SetActive(_AreInstructions1Active);
        instructions2Menu.SetActive(_AreInstructions2Active);
    }

    public void whenInstructions2ClickedBack()
    {
        _AreInstructions2Active = false;
        instructions2Menu.SetActive(_AreInstructions2Active);
    }

    public void whenTextSliderChanged()
    {
        GameSettings gameSettings = GameObject.FindObjectOfType<GameSettings>();
        gameSettings.FramesPerCharacter = (int)Mathf.Clamp((1 - textSpeedSlider.value) * 10, 1, 10);
        Debug.Log(gameSettings.FramesPerCharacter.ToString());
    }


}
