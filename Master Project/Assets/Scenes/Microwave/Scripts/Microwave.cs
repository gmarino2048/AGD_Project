using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Microwave : MonoBehaviour {

    private int counter;
    public Text TimerText;

	// Use this for initialization
	void Start () {
        counter = 300;
        InvokeRepeating("Countdown", 1, 1);
        TimerText.text = "00:" + counter.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Countdown()
    {
        counter--;
        TimerText.text = "00:" + counter.ToString();
    }
}
