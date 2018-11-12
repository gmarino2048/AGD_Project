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

        void Awake()
        {
            showup = false;
        }
        public void Update()
        {
            if ((bar.value >= 0.85) && (!beghandler.begshown))
            {
                beg.SetActive(false);
                beghandler.isActive = false;
            }
            else
            {
                beg.SetActive(true);
            }
        }
        //Make sure to attach these Buttons in the Inspector
        public void TalkHandler()
        {
            Info.SetActive(false);
        }
    }
}
