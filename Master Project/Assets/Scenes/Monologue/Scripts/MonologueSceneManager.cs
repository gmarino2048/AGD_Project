using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Dialog;
using Monsters;

namespace Monologue
{
    public class MonologueSceneManager : MonoBehaviour
    {
        /// <summary>
        /// The Text element to use to display the monologue.
        /// </summary>
        public Text textDisplay;

        private readonly string _WIN_SCENE_NAME = "FinalChoice";
        private readonly string _LOSE_SCENE_NAME = "Main Menu";
        private readonly string _DIALOGUE_SCENE_NAME = "DialogueScene";

        private Queue<string> _MonologueEntries;
        private bool _IsTypingText = false;
        private string _NextSceneName;
        private GameSettings _GameSettings;

        // Use this for initialization
        void Start()
        {
            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings == null)
            {
                throw new Exception("GameSettings did not exist in scene");
            }

            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            var monologue = gameNarrativeManager.GetAppropriateMonologue();

            if (monologue.IsForEnd)
            {
                if (monologue.IsForWin) _NextSceneName = _WIN_SCENE_NAME;
                else _NextSceneName = _LOSE_SCENE_NAME;
            }
            else
            {
                _NextSceneName = _DIALOGUE_SCENE_NAME;
                gameNarrativeManager.StartNextStage();
            }

            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings == null)
            {
                throw new Exception("GameSettings did not exist in scene");
            }

            _MonologueEntries = new Queue<string>(monologue.Entries);

            ShowNextEntry();
        }

        public bool ShowNextEntry(bool shouldStopTyping = false)
        {
            if (!_MonologueEntries.Any())
            {
                return false;
            }

            if (_IsTypingText)
            {
                if (shouldStopTyping)
                {
                    _IsTypingText = false;
                    StopAllCoroutines();
                }
                else
                {
                    return true;
                }
            }

            StartCoroutine(TypeText(_MonologueEntries.Dequeue()));
            return true;
        }

        public void EndScene()
        {
            SceneManager.LoadScene(_NextSceneName, LoadSceneMode.Single);
        }

        private IEnumerator TypeText(string textToType)
        {
            _IsTypingText = true;

            textDisplay.text = string.Empty;
            foreach (char letter in textToType)
            {
                if (!_IsTypingText)
                {
                    textDisplay.text = textToType;
                    break;
                }

                textDisplay.text += letter;
                for (var i = 0; i < _GameSettings.FramesPerCharacter; i++)
                {
                    yield return new WaitForSecondsRealtime(Time.deltaTime * _GameSettings.FramesPerCharacter);
                }
                yield return null;
            }
            
            _IsTypingText = false;
        }
    }
}