using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class TimerBehavior : MonoBehaviour
    {

        [Header("Game Time")]
        public float GameTime = 10f;
        public float TimeRemaining { get; private set; }

        [Header("Time Display")]
        public TextMesh TextMesh;

        [Header("Game Objects")]
        public MicrowaveController Microwave;

        public bool GameActive { get; private set; }
        public bool GameComplete { get; private set; }

        // Use this for initialization
        void Start()
        {
            TimeRemaining = GameTime;

            TextMesh.text = Format(TimeRemaining);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameActive && TimeRemaining > 0)
            {
                TextMesh.text = Format(TimeRemaining);
                TimeRemaining -= Time.deltaTime;
            }
            else if (TimeRemaining <= 0) {
                GameActive = false;
                GameComplete = true;

                TextMesh.text = Format(0f);
            }
        }

        public void Activate () 
        {
            Microwave.Play();
            GameActive = true;
        }

        public void Stop () 
        {

        }

        string Format(float timeRemaining) 
        {
            int seconds = Mathf.FloorToInt(timeRemaining);
            string firstPart = seconds.ToString("D2");

            int millis = Mathf.FloorToInt((timeRemaining - seconds) * 100f);
            string secondPart = millis.ToString("D2");

            return firstPart + ":" + secondPart;
        }
    }
}
