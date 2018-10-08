using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenePickerBehavior : MonoBehaviour
{

    [Header("Chopping")]

    [SerializeField]
    public Button ChoppingButton; // The button to press for the Chopping scene
    public string ChoppingName; // The name of the chopping scene

    [Header("Combat")]

    [SerializeField]
    public Button CombatButton; // The button to press for the combat scene
    public string CombatName; // The name of the combat scene

    [Header("Dialogue")]

    [SerializeField]
    public Button DialogueButton; // The button to press for the dialogue scene
    public string DialogueName; // The name of the dialogue scene

    [Header("Microwave")]

    [SerializeField]
    public Button MicrowaveButton; // The button to press for the microwave scene
    public string MicrowaveName; // The name of the microwave scene

    [Header("Shaking")]

    [SerializeField]
    public Button ShakingButton; // The button to press for the shaking scene
    public string ShakingName; // The name of the shaking scene

    [Header("Grill")]
    [SerializeField]
    public Button GrillButton; // The button to press for the shaking scene
    public string GrillName; // The name of the shaking scene


    /// <summary>
    /// Adds the listeners to each individual button so that when they are pressed
    /// the correct scene loads from the list of available scenes.
    /// </summary>
    private void Start()
    {
        ChoppingButton.onClick.AddListener(OnChoppingPressed);

        CombatButton.onClick.AddListener(OnCombatPressed);

        DialogueButton.onClick.AddListener(OnDialoguePressed);

        MicrowaveButton.onClick.AddListener(OnMicrowavePressed);

        ShakingButton.onClick.AddListener(OnShakingPressed);

        GrillButton.onClick.AddListener(OnGrillPressed);
    }

    /// <summary>
    /// Loads the chopping scene.
    /// </summary>
    private void OnChoppingPressed ()
    {
        SceneManager.LoadScene(ChoppingName);
    }


    /// <summary>
    /// Loads the combat scene.
    /// </summary>
    private void OnCombatPressed()
    {
        SceneManager.LoadScene(CombatName);
    }


    /// <summary>
    /// Loads the dialogue scene.
    /// </summary>
    private void OnDialoguePressed()
    {
        SceneManager.LoadScene(DialogueName);
    }


    /// <summary>
    /// Loads the microwave scene.
    /// </summary>
    private void OnMicrowavePressed()
    {
        SceneManager.LoadScene(MicrowaveName);
    }


    /// <summary>
    /// Loads the shaking scene.
    /// </summary>
    private void OnShakingPressed()
    {
        SceneManager.LoadScene(ShakingName);
    }


    /// <summary>
    /// Loads the grill scene.
    /// </summary>
    private void OnGrillPressed()
    {
        SceneManager.LoadScene(ShakingName);
    }
}
