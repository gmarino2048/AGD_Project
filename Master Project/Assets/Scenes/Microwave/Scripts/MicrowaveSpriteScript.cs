using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveSpriteScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //myObject.GetComponent<MyScript>().MyFunction();
        GameObject.Find("TimerText").GetComponent<MicrowaveTimerScript>().buttonClicked();
    }
}
