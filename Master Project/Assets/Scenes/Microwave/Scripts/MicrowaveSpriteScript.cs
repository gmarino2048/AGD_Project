using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class MicrowaveSpriteScript : MonoBehaviour
    {
        private void OnMouseDown()
        {
            //myObject.GetComponent<MyScript>().MyFunction();
            GameObject.Find("TimerText").GetComponent<MicrowaveTimerScript>().OnButtonClicked();
        }
    }
}
