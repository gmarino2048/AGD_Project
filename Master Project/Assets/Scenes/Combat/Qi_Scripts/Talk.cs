using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class Talk : MonoBehaviour
    {
        public GameObject Info;
        public GameObject beg;
        public BegHandler beghandler;
        public Slider bar;
        public bool showup;

        private void Awake()
        {
            showup = false;
        }
        public void Update()
        {
            if ((bar.value >= 0.85) && (!beghandler.begshown))
            {
                //Debug.Log(bar.value);
                beg.SetActive(true);
            }
            else
            {
                beg.SetActive(false);
            }
        }
        //Make sure to attach these Buttons in the Inspector
        public void TalkHandler()
        {
            //Output this to console when the Button is clicked
            Debug.Log("Open talk options");
            showup = true;
            Info.SetActive(true);
        }
    }
}
