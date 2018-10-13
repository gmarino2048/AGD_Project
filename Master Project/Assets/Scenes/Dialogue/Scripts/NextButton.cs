using UnityEngine;

public class NextButton : MonoBehaviour {
    public void OnClick() {
        var dialogueManager = FindObjectOfType<DialogueManager>();
        if (!dialogueManager.GoToNextPrompt() && !dialogueManager.ShowResponseOptions()) {
            dialogueManager.EndDialogue();
        }
    }
}
