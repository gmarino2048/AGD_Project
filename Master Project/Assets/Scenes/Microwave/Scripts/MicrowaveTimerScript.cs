using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microwave
{
    public class MicrowaveTimerScript : MonoBehaviour
    {
        //how much time is left
        private int Counter;
        //textbox with countdown
        public Text TimerText;
        //gets the score of the minigame
        public MicrowaveScorekeeper Scorekeeper;
        //whether the game is still going
        private bool StillRunning;

        Animator anim;

        /// <summary>
        /// Start this instance.
        /// initializes variables
        /// starts timer
        /// </summary>
        void Start()
        {
            anim = GameObject.Find("Microwave Sprite").GetComponent<Animator>();
        }


        public void StartGame()
        {
            Counter = 10;
            //every second call countdown method (starts after a second)
            InvokeRepeating("Countdown", 1, 1);
            TimerText.text = "00:" + Counter.ToString("D2");
            StillRunning = true;
        }

        /// <summary>
        /// assuming game hasn't finished, decrements counter
        /// also updates textbox
        /// </summary>
        void Countdown()
        {
            if (Counter > 0)
            {
                if (StillRunning == true)
                {
                    Counter--;
                    TimerText.text = "00:" + counter.ToString("D2");
                }
            }
            else {
                FinishMicrowaveGame();
            }

        }

        /// <summary>
        /// if GUI button to stop microwave is clciked
        /// stops timer
        /// stops game
        /// </summary>
        public void buttonClicked()
        {
            StillRunning = false;
            anim.SetTrigger("Open");
            FinishMicrowaveGame();
        }

        /// <summary>
        /// after game is done, this is called
        /// not sure what to do here yet
        /// </summary>
        private void FinishMicrowaveGame()
        {
            var ScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
            ScoreManager.AddIngredientToDish(new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}"), IngredientType.AlgaeSlime, Scorekeeper.Score());
        }
    }

}
