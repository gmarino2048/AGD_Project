using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour {

    public float InitialTime = 30f; // Set timer for 30s by default.

    public float CurrentTime { get; private set; } // The current time of the countdown.
    Text Text; // The timer's text.

	/// <summary>
    /// Runs on start of the scene.
    /// </summary>
	void Start () {
        CurrentTime = InitialTime;
        Text = GetComponentInChildren<Text>();
	}
	
	/// <summary>
    /// Runs Once per frame
    /// </summary>
	void Update () {
        if (CurrentTime > 0) {
            Text.text = ((int)CurrentTime).ToString();
            CurrentTime = CurrentTime - Time.deltaTime;
        }
	}
}
