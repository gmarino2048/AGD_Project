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

	// Use this for initialization
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
	
    void OnMouseDown()
    {
        if(!isFinished)
        {
            isSelected = true;

            offset = this.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("is selected");
        }
    }

    void OnMouseUp()
    {
        isSelected = false;

        Debug.Log("is unselected");

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
    }

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

	// Update is called once per frame
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
