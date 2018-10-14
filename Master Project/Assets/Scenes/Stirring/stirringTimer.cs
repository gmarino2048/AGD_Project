using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stirring
{
    public class stirringTimer : MonoBehaviour
    {
        //how much time is left
        public int Counter;
        //textbox with countdown
        public Text TimerText;
        //whether the game is still going
        private bool StillRunning;

        /// <summary>
        /// Start this instance.
        /// initializes variables
        /// starts timer
        /// </summary>
        void Start()
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
                    TimerText.text = "00:" + Counter.ToString("D2");
                }
            }
            else
            {
                GameObject.Find("spoon").GetComponent<spoonScript>().timerDone = true;
                FinishStirringGame();
            }

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
            GameObject.Find("Scorekeeper").GetComponent<ScoreKeeperScript>().sendScore();

        }
    }
}

