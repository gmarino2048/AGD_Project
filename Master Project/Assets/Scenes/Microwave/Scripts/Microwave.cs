using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microwave
{
    public class Microwave : MonoBehaviour
    {

        private int Counter;
        public Text TimerText;

        // Use this for initialization
        void Start()
        {
            Counter = 300;
            InvokeRepeating("Countdown", 1, 1);
            TimerText.text = "00:" + Counter.ToString("D2");
        }


        void Countdown()
        {
            Counter--;
            TimerText.text = "00:" + Counter.ToString("D2");
        }
    }
}

