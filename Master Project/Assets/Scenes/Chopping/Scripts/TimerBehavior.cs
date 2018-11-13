using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chopping
{
    public class TimerBehavior : MonoBehaviour
    {

        [Header("Timer Settings")]
        public uint GameTime = 30;
        float RemainingTime;

        public bool GameActive { get; private set; }
        public bool GameComplete { get; private set; }

        [Header("Timer UI Elements")]
        public Text TimerText;

        // Use this for initialization
        void Start()
        {
            GameActive = false;
            RemainingTime = GameTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameActive && RemainingTime > 0)
            {
                TimerText.text = Mathf.RoundToInt(RemainingTime).ToString();

                RemainingTime -= Time.deltaTime;
            }
            else if (GameActive) 
            {
                GameComplete = true;
                GameActive = false;

                EndGame();
            }
        }

        public void Activate () 
        {
            GameActive = true;
        }

        public void EndGame () 
        {

        }
    }
}
