using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microwave
{
    public class MicrowaveTimerScript : MonoBehaviour, IDishScoreKeeper
    {
        private readonly Guid _NESSIE_GUID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");

        //how much time is left
        private int _Counter;

        //whether the game is still going
        private bool _IsStillRunning;

        /// <summary>
        /// The dish preparation manager
        /// </summary>
        private DishPreparationManager _DishPreparationManager;

        /// <summary>
        /// The dish score manager
        /// </summary>
        private DishScoreManager _DishScoreManager;

        //textbox with countdown
        public Text timerText;

        /// <summary>
        /// The animator for the microwave
        /// </summary>
        public Animator anim;

        /// <summary>
        /// Start this instance.
        /// initializes variables
        /// starts timer
        /// </summary>
        void Start()
        {
            anim = GameObject.Find("Microwave Sprite").GetComponent<Animator>();
            _DishPreparationManager = GameObject.FindObjectOfType<DishPreparationManager>();
            _DishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
        }

        public void StartGame()
        {
            _Counter = 10;
            //every second call countdown method (starts after a second)
            InvokeRepeating("Countdown", 1, 1);
            UpdateTimerText();
            _IsStillRunning = true;
        }

        /// <summary>
        /// assuming game hasn't finished, decrements counter
        /// also updates textbox
        /// </summary>
        void Countdown()
        {
            if (_Counter > 0)
            {
                if (_IsStillRunning == true)
                {
                    _Counter--;
                    UpdateTimerText();
                }
            }
            else {
                FinishMicrowaveGame();
            }
        }

        /// <summary>
        /// Gets the score for the minigame
        /// </summary>
        /// <returns>A number between 0 and 1 representing the score for the minigame.</returns>
        public float GetScore() {
            return 0.5f; //TODO
        }

        /// <summary>
        /// if GUI button to stop microwave is clciked
        /// stops timer
        /// stops game
        /// </summary>
        public void OnButtonClicked()
        {
            _IsStillRunning = false;
            anim.SetTrigger("Open");
            FinishMicrowaveGame();
        }

        /// <summary>
        /// Updates the text for the timer using proper formatting
        /// </summary>
        private void UpdateTimerText()
        {
            timerText.text = "00:" + _Counter.ToString("D2");
        }

        /// <summary>
        /// after game is done, this is called
        /// not sure what to do here yet
        /// </summary>
        private void FinishMicrowaveGame()
        {
            StartCoroutine(EndMiniGame());
        }

        /// <summary>
        /// Sends the score and goes to the next scene
        /// </summary>
        private IEnumerator EndMiniGame()
        {
            yield return new WaitForSeconds(10);

            if (_DishPreparationManager != null)
            {
                if (_DishScoreManager != null)
                {
                    _DishScoreManager.AddIngredientToDish(_NESSIE_GUID, _DishPreparationManager.currentIngredient, GetScore());
                }

                _DishPreparationManager.GoToNextScene();
            }
        }
    }
}
