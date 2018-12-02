using Dialog;
using Monsters;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Dialogue
{
    /// <summary>
    /// The DialogueManager class that manages the Dialogue GUI
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        /// <summary>
        /// The name of the next scene.
        /// </summary>
        public string nextSceneName;

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

        public MonsterController Patron;

        public Font standardFont;
        public Font internalFont;

        private DialogDataManager _DialogDataManager;
        private GameSettings _GameSettings;
        private MonsterData _MonsterData;
        private Prompt _CurrentPrompt;

        private int _ResponsesScore = 0;
        private int _TotalPossibleResponseScore = 0;

        /// <summary>
        /// Starts and displays the dialogue for each sentence in the list of sentences
        /// </summary>
        /// <param name="monsterId">The ID of the monster to start a dialogue with</param>
        public void StartDialogue(Guid monsterId)
        {
            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            if (monsterFactory == null)
            {
                throw new Exception("MonsterFactory did not exist in scene");
            }

            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings == null)
            {
                throw new Exception("GameSettings did not exist in scene");
            }

            _MonsterData = monsterFactory.LoadMonster(monsterId);
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

            switch (chosenResponse.Value)
            {
                case -1:
                    Patron.TriggerBad();
                    break;
                case 1:
                    Patron.TriggerGood();
                    break;
            }

            _ResponsesScore += chosenResponse.Value;

            _CurrentPrompt = _DialogDataManager.Prompts[chosenResponse.NextPromptID];
            DisplayPromptBody();
        }

        /// <summary>
        /// Shows response options for the current prompt if possible
        /// </summary>
        /// <returns>Returns true if response options were available, false otherwise</returns>
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
        /// Ends the dialogue. Updates score for the appropriate monster and 
        /// </summary>
        public void EndDialogue()
        {
            _MonsterData.UpdateAffectionFromConversationScore(GetConversationScore());
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }

        /// <summary>
        /// Scores the conversation as a whole
        /// </summary>
        /// <returns>A number between 0 and 1. 0 is best, 1 is worst.</returns>
        private float GetConversationScore()
        {
            if (_TotalPossibleResponseScore == 0)
                return 0;

            float conversationScore = (_TotalPossibleResponseScore - _ResponsesScore) / (2.0f * _TotalPossibleResponseScore);
            return conversationScore;
        }

        /// <summary>
        /// Displays the current prompts body.
        /// </summary>
        private void DisplayPromptBody()
        {
            responsesDisplay.SetActive(false);
            continueButton.SetActive(false);

            var speakerName = _MonsterData.Name;
            dialogueText.font = standardFont;

            if (_CurrentPrompt.IsSaidByPlayer)
            {
                if (_CurrentPrompt.IsInternalMonologue)
                {
                    speakerName = "Internal";
                    dialogueText.font = internalFont;
                }
                else
                {
                    speakerName = _GameSettings.PlayerName;
                }
            }

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

            int maxResponseScore = 0;

            var responseSet = _DialogDataManager.ResponseSets[_CurrentPrompt.ResponseSetID];
            for (int i = 0; i < responseSet.Length; i++)
            {
                var response = responseSet[i];
                var responseButton = responseButtons[i];

                if (response.Value > maxResponseScore)
                    maxResponseScore = response.Value;

                responseButton.GetComponentInChildren<Text>().text = response.Body;
                responseButton.onClick.AddListener(() => GoToNextPrompt(response));
            }

            _TotalPossibleResponseScore += maxResponseScore;

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
            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            try
            {
                StartDialogue(gameNarrativeManager.CurrentStage.MonsterID);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message + " -- Scene Not Running in Game");
            }
        }
    }
}
