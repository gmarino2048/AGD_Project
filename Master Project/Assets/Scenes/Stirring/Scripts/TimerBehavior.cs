using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stirring
{
    public class TimerBehavior : MonoBehaviour
    {

        [Header("Time Controls")]
        public int GameTime;
        public float TimeRemaining { get; private set; }

        public bool GameActive { get; private set; }
        public bool GameComplete { get; private set; }

        [Header("Time Display")]
        public Text TimerText;
        public string Message = "Time Remaining: ";

        // Use this for initialization
        void Start()
        {
            TimeRemaining = GameTime;

            TimerText.text = Message + GameTime.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameActive && TimeRemaining > 0)
            {
                int remainingTime = Mathf.RoundToInt(TimeRemaining);
                string display = Message + remainingTime.ToString();

                TimerText.text = display;

                TimeRemaining -= Time.deltaTime;
            }
            else if (GameActive)
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            GameActive = false;
            GameComplete = true;
        }

        public void Activate()
        {
            GameActive = true;
        }
    }
}