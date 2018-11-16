using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class MicrowaveController : MonoBehaviour
    {

        [Header("Microwave Animation Controller")]
        public Animator MicrowaveAnimator;
        public float CriticalTime;

        [Header("Animation Parameters")]
        public string Continue = "Continue";
        public string Opened = "Opened";

        public enum MicrowaveState
        {
            Stopped,
            Loop,
            Erratic,
            Opened
        }

        public MicrowaveState CurrentState { get; private set; }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartAnimation () 
        {

        }
    }
}