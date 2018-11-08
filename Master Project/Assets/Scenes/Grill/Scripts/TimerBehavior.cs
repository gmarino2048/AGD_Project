using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grill
{
    public class TimerBehavior : MonoBehaviour
    {

        [Header("Timer Text")]
        public string Message = "Time Remaining:";
        public Text TimerText;

        [Header("Initial Time")]
        public int InitialTime = 30;
        public float TimeRemaining { get; private set; }

        public bool GameActive { get; private set; }
        public bool GameOver { get; private set; }

        // Use this for initialization
        void Start()
        {
            TimeRemaining = InitialTime;
            GameActive = false;
            GameOver = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameActive && TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                int currentTime = Mathf.RoundToInt(TimeRemaining);
                string toDisplay = Message + " " + currentTime.ToString();

                TimerText.text = toDisplay;
            }
            else if (!GameOver && GameActive)
            {
                GameActive = false;
                GameOver = true;
            }
        }

        public void Activate () {
            GameActive = true;
        }
    }
}
