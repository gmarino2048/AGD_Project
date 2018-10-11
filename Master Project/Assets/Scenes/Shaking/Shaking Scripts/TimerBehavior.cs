using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shaking
{
    public class TimerBehavior : MonoBehaviour
    {

        [Header("Text Options")]
        [SerializeField]
        public Text HeaderText; // The Header text for the timer icon.
        public string HeaderValue = "Timer:"; // The value for the header timer.
        [SerializeField]
        public Text TimerText; // The value that shows the current value of the timer.
        public string CompleteValue = "Minigame Complete"; // The value to show when the timer finishes.

        [Header("Initial Value")]
        public float TotalTime; // The total time to count for.

        public float CurrentTime { get; private set; } // The current value for the time remaining.
        public bool Finished { get; private set; } // Set to True when the timer has finished.

        /// <summary>
        /// Sets the start time and the finished value to false.
        /// </summary>
        void Start()
        {
            CurrentTime = TotalTime;
            Finished = false;
        }

        
        /// <summary>
        /// Controls the timer countdown and sets the timer to done when the minigame is
        /// complete. Also changes the timer text on minigame completion.
        /// </summary>
        void Update()
        {
            if (CurrentTime > 0)
            {
                TimerText.text = Mathf.RoundToInt(CurrentTime).ToString();
                CurrentTime -= Time.deltaTime;
            }
            else if (Finished == false)
            {
                Finished = true;
                HeaderText.text = CompleteValue;
                TimerText.text = "";
            }
        }
    }
}
