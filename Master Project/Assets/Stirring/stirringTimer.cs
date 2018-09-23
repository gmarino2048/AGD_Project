using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stirringTimer : MonoBehaviour
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
        counter = 30;
        //every second call countdown method (starts after a second)
        InvokeRepeating("Countdown", 1, 1);
        TimerText.text = "00:" + counter.ToString();
        stillRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

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
        else
        {
            FinishStirringGame();
        }

    }


    /// <summary>
    /// after game is done, this is called
    /// not sure what to do here yet
    /// </summary>
    private void FinishStirringGame()
    {
        GameObject thePlayer = GameObject.Find("spoon");
        spoon playerScript = thePlayer.GetComponent<spoon>();
        float distance =  playerScript.travelDistance;
    }
}
