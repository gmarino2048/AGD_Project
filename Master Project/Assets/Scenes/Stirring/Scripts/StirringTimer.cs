using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stirring
{
    public class StirringTimer : MonoBehaviour
    {
        //how much time is left
        public int counter;

        //textbox with countdown
        public Text timerText;

        //whether the game is still going
        private bool _IsStillRunning;

        /// <summary>
        /// Start this instance.
        /// initializes variables
        /// starts timer
        /// </summary>
        void Start()
        {
            counter = 10;
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
            if (counter > 0 && _IsStillRunning)
            {
                counter--;
                UpdateTimerText();
            }
            else
            {
                CancelInvoke();
                FinishStirringGame();
            }
        }

        /// <summary>
        /// Updates the text for the timer using proper formatting
        /// </summary>
        private void UpdateTimerText()
        {
            timerText.text = "00:" + counter.ToString("D2");
        }

        /// <summary>
        /// after game is done, this is called
        /// not sure what to do here yet
        /// </summary>
        private void FinishStirringGame()
        {
            /*GameObject thePlayer = GameObject.Find("spoon");
            spoon spoonScript = thePlayer.GetComponent<spoon>();
            float distance =  spoonScript.travelDistance;*/
            //Debug.Log("hi");
            GameObject.Find("spoon").GetComponent<SpoonScript>().isTimerDone = true;
            _IsStillRunning = false;
            GameObject.Find("Scorekeeper").GetComponent<ScoreKeeperScript>().SendScore();
        }
    }
}

