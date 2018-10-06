using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveTimerScript : MonoBehaviour
{
    //how much time is left
    private int counter;
    //textbox with countdown
    public Text TimerText;
    //whether the game is still going
    private bool stillRunning;

    /// <summary>
    /// Start this instance.
    /// initializes variables
    /// starts timer
    /// </summary>
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        counter = 10;
        //every second call countdown method (starts after a second)
        InvokeRepeating("Countdown", 1, 1);
        TimerText.text = "00:" + counter.ToString();
        stillRunning = true;
    }

    /// <summary>
    /// assuming game hasn't finished, decrements counter
    /// also updates textbox
    /// </summary>
    void Countdown()
    {
        if (counter > 0)
        {
            if (stillRunning == true)
            {
                counter--;
                TimerText.text = "00:" + counter.ToString();
            }
        }
        else{
            FinishMicrowaveGame();
        }
        
    }

    /// <summary>
    /// if GUI button to stop microwave is clciked
    /// stops timer
    /// stops game
    /// </summary>
   public void buttonClicked(){
        stillRunning = false;
        FinishMicrowaveGame();
    }

    /// <summary>
    /// after game is done, this is called
    /// not sure what to do here yet
    /// </summary>
    private void FinishMicrowaveGame(){

    }
}
