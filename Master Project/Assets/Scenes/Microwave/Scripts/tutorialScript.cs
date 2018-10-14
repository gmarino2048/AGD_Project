using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class tutorialScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }


        private void OnMouseDown()
        {
            this.transform.Translate(Vector3.forward * 100);
            GameObject.Find("TimerText").GetComponent<MicrowaveTimerScript>().StartGame();
        }
    }
}
