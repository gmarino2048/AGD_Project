using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class MicrowaveSpriteScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }


        private void OnMouseDown()
        {
            //myObject.GetComponent<MyScript>().MyFunction();
            GameObject.Find("TimerText").GetComponent<MicrowaveTimerScript>().buttonClicked();
        }
    }

}
