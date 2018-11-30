using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Monologue
{
    public class MonologueSceneManager : MonoBehaviour
    {
        /// <summary>
        /// The Text element to use to display the monologue.
        /// </summary>
        public Text textDisplay;

        /// <summary>
        /// The scene to go to after the monologue is over.
        /// </summary>
        public string nextSceneName;

        private Queue<string> _MonologueEntries;
        private bool _IsTypingText = false;
        private bool _WasKeyDown = false;

        // Use this for initialization
        void Start()
        {
            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            try
            {
                gameNarrativeManager.StartNextStage();
                _MonologueEntries = new Queue<string>(gameNarrativeManager.CurrentStage.Monologue.Entries);
            }
            catch (Exception)
            {
                Debug.LogWarning("Scene not running in game");
            }

            ShowNextEntry();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKey)
            {
                if (_WasKeyDown)
                {
                    return;
                }
                _WasKeyDown = true;

                if (_IsTypingText)
                {
                    _IsTypingText = false;
                    return;
                }
                try
                {
                    if (!_MonologueEntries.Any())
                    {
                        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
                        return;
                    }
                }
                catch (Exception)
                {
                    Debug.LogWarning("Scene not running in game");
                }

                ShowNextEntry();
            }
            else
            {
                _WasKeyDown = false;
            }
        }

        private void ShowNextEntry()
        {
            try
            {
                StopAllCoroutines();
                StartCoroutine(TypeText(_MonologueEntries.Dequeue()));
            }
            catch (Exception)
            {
                StopAllCoroutines();
                StartCoroutine(TypeText("Scene not running in game"));
            }
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
                yield return null;
            }

            _IsTypingText = false;
        }
    }
}