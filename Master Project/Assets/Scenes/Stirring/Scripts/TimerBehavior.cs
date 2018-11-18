using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour {

    [Header("Time Controls")]
    public int GameTime;
    float TimeRemaining;

    public bool GameActive { get; private set; }
    public bool GameComplete { get; private set; }

    [Header("Time Display")]
    public Text TimerText;
    public string Message = "Time Remaining: ";

	// Use this for initialization
	void Start () {
        TimeRemaining = GameTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameActive && TimeRemaining > 0)
        {
            int remainingTime = Mathf.RoundToInt(TimeRemaining);
            string display = Message + remainingTime.ToString();

            TimerText.text = display;

            TimeRemaining -= Time.deltaTime;
        }
        else if (GameActive) 
        {
            GameActive = false;
            GameComplete = true;

            EndGame();
        }
	}

    public void EndGame()
    {

    }

    public void Activate () 
    {
        GameActive = true;
    }
}
