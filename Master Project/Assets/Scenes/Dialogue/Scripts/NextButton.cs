using UnityEngine;

namespace Dialogue
{
    public class NextButton : MonoBehaviour
    {
        public bool shouldSkipDialogue = false;

        public void OnClick()
        {
            var dialogueManager = FindObjectOfType<DialogueManager>();
            if (shouldSkipDialogue || (!dialogueManager.GoToNextPrompt() && !dialogueManager.ShowResponseOptions()))
            {
                dialogueManager.EndDialogue();
            }
        }
    }
}
