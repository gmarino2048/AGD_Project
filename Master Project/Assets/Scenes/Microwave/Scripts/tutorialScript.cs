using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        this.transform.Translate(Vector3.forward * 100);
        GameObject.Find("TimerText").GetComponent<MicrowaveTimerScript>().StartGame();
    }
}
