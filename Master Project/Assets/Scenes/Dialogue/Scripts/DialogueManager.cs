using Dialog;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The DialogueManager class that manages the Dialogue GUI
/// </summary>
public class DialogueManager : MonoBehaviour {
    
    /// <summary>
    /// The text for the name of the character for the dialogue
    /// </summary>
    public Text nameText;
    
    /// <summary>
    /// The text for the name of the character for the dialogue
    /// </summary>
    public Text dialogueText;

    /// <summary>
    /// The response buttons.
    /// </summary>
    public Button[] responseButtons;

    /// <summary>
    /// The prompt display.
    /// </summary>
    public GameObject promptDisplay;

    /// <summary>
    /// The responses display.
    /// </summary>
    public GameObject responsesDisplay;

    /// <summary>
    /// The continue button.
    /// </summary>
    public GameObject continueButton;

    private DialogDataManager _DialogDataManager;
    private Prompt _CurrentPrompt;

    private readonly Guid _NESSIE_ID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");

    /// <summary>
    /// Starts and displays the dialogue for each sentence in the list of sentences
    /// </summary>
    /// <param name="monsterId">The ID of the monster to start a dialogue with</param>
    public void StartDialogue(Guid monsterId)
	{
        _DialogDataManager = DialogDataManager.LoadFromXml(monsterId);
        _CurrentPrompt = _DialogDataManager.Prompts[_DialogDataManager.InitialPromptID];

        DisplayPromptBody();
	}

    /// <summary>
    /// Goes to the next prompt if there is one to show.
    /// </summary>
    /// <returns>Returns true if it was able to move forward, false if there was no next prompt.</returns>
    public bool GoToNextPrompt()
    {
        if (string.IsNullOrEmpty(_CurrentPrompt.NextPromptID))
        {
            return false;
        }

        _CurrentPrompt = _DialogDataManager.Prompts[_CurrentPrompt.NextPromptID];
        DisplayPromptBody();

        return true;
    }

    /// <summary>
    /// Goes to the next prompt given a chosen response.
    /// </summary>
    /// <param name="chosenResponse">The chosen response.</param>
    public void GoToNextPrompt(Response chosenResponse)
    {
        ResetResponseButtonEventHandlers();

        _CurrentPrompt = _DialogDataManager.Prompts[chosenResponse.NextPromptID];
        DisplayPromptBody();
    }

    public bool ShowResponseOptions()
    {
        if (string.IsNullOrEmpty(_CurrentPrompt.ResponseSetID))
        {
            return false;
        }

        DisplayResponseOptions();
        return true;
    }
    
    /// <summary>
    /// Displays the current prompts body.
    /// </summary>
	private void DisplayPromptBody()
	{
        responsesDisplay.SetActive(false);
        continueButton.SetActive(false);

        var speakerName = _CurrentPrompt.IsSaidByPlayer ? "You" : "Nessie"; //TODO: Replace "Nessie" with the actual name of the monster.

		StopAllCoroutines();
		StartCoroutine(TypeSentence(speakerName, _CurrentPrompt.Body));

        promptDisplay.SetActive(true);
	}

    /// <summary>
    /// Resets the response button event handlers.
    /// </summary>
    private void ResetResponseButtonEventHandlers()
    {
        foreach (var responseButton in responseButtons)
        {
            responseButton.onClick.RemoveAllListeners();
        }
    }

    /// <summary>
    /// Displays the response options.
    /// </summary>
    private void DisplayResponseOptions()
    {
        promptDisplay.SetActive(false);
        continueButton.SetActive(false);

        var responseSet = _DialogDataManager.ResponseSets[_CurrentPrompt.ResponseSetID];
        for (int i = 0; i < responseSet.Length; i++)
        {
            var response = responseSet[i];
            var responseButton = responseButtons[i];

            responseButton.GetComponentInChildren<Text>().text = response.Body;
            responseButton.onClick.AddListener(() => GoToNextPrompt(response));
        }
        
        responsesDisplay.SetActive(true);
    }

    /// <summary>
    /// The sentence is typed character by character
    /// </summary>
    /// <param name="speakerName">The name of the speaker that is saying the sentence</param>
    /// <param name="sentence">The sentence to type</param>
    private IEnumerator TypeSentence(string speakerName, string sentence)
    {
        nameText.text = speakerName;

        dialogueText.text = string.Empty;
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }

        continueButton.SetActive(true);
    }

    public void Start()
    {
        StartDialogue(_NESSIE_ID);
    }
}
