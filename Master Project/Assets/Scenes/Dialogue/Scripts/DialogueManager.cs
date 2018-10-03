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

    private DialogDataManager _DialogDataManager;
    private Prompt _CurrentPrompt;
    
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
        _CurrentPrompt = _DialogDataManager.Prompts[chosenResponse.NextPromptID];
        DisplayPromptBody();
    }
    
    /// <summary>
    /// Displays the current prompts body.
    /// </summary>
	private void DisplayPromptBody()
	{
        var speakerName = _CurrentPrompt.IsSaidByPlayer ? "You" : "Customer"; //TODO: Replace "Customer" with the actual name of the monster.

		StopAllCoroutines();
		StartCoroutine(TypeSentence(speakerName, _CurrentPrompt.Body));

        if (!string.IsNullOrEmpty(_CurrentPrompt.ResponseSetID))
        {
            // TODO: Display response options
        }
	}

    /// <summary>
    /// The sentence is typed character by character
    /// </summary>
    /// <param name="speakerName">The name of the speaker that is saying the sentence</param>
    /// <param name="sentence">The sentence to type</param>
    private IEnumerator TypeSentence (string speakerName, string sentence)
	{
		dialogueText.text = string.Empty;
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}
}
