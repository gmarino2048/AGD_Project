using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerScript : MonoBehaviour
{
    public Camera mainCamera;
    public Text shakeCountDisplay;
    public Text timer;
 
    private Vector3 offset;
    private int shakeCounter;
    private float countdownMax;
    private Collider2D lastEnteredZone;
    private bool isSelected;
    private bool isStarted;
    private bool isFinished;

	/// <summary>
    /// Start configures most elements in the scene, including instance variables, text for the text boxes, and counters
    /// </summary>
	void Start () {
        isSelected = false;
        isStarted = false;
        isFinished = false;
        
        shakeCounter = -1;
        countdownMax = 15.0f;
        shakeCountDisplay.text = shakeCounter.ToString();
        timer.text = countdownMax.ToString() + " Seconds Left";
        lastEnteredZone = new Collider2D();
	}
	
    /// <summary>
    /// This method detects when you pick up the shaker, and starts the minigame
    /// </summary>
    void OnMouseDown()
    {
        if(!isFinished)
        {
            isSelected = true;

            offset = this.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("is selected");
        }
    }

    /// <summary>
    /// this method determines if the shaker is let go
    /// </summary>
    void OnMouseUp()
    {
        isSelected = false;

        Debug.Log("is unselected");

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
    }

    /// <summary>
    /// This method is called when the rigidbody attached to the shaker collides with one of the hitboxes of the "shaking" zones.
    /// It saves a pointer to the last entered zone in order to make sure you're colliding with the other zone next time,
    /// and counts the number of collisions, which forces you to make a shaking motion.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Entered");
        if(other != lastEnteredZone)
        {
            lastEnteredZone = other;
            shakeCounter++;
            shakeCountDisplay.text = shakeCounter.ToString() + " Shakes";
        }
    }

	/// <summary>
    /// Update handles the movement of the shaker via the isSelected variable, as well as handles the countdown timer once
    /// the minigame begins.
    /// </summary>
	void Update ()
    {
        if(isSelected)
        {
            this.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 2f) + offset;
            isStarted = true;
        }

        if(isStarted)
        {
            if(countdownMax > 0f)
            {
                countdownMax -= Time.deltaTime;
                timer.text = Mathf.Ceil(countdownMax).ToString() + " Seconds Left";
                
            }
            else
            {
                isFinished = true;
                isSelected = false;
            }
        }		    
	}
}
