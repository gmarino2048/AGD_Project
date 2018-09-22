using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Talk : MonoBehaviour {

    public GameObject Info;
    public bool showup;

    private void Awake()
    {
        showup = false;
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
