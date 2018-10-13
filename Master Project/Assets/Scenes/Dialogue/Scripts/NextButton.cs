using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour {
    /// <summary>
    /// The name of the next scene.
    /// </summary>
    public string nextSceneName;

    public void OnClick() {
        var dialogueManager = FindObjectOfType<DialogueManager>();
        if (!dialogueManager.GoToNextPrompt() && !dialogueManager.ShowResponseOptions()) {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }
}
