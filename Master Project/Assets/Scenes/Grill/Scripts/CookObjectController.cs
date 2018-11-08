using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookObjectController : MonoBehaviour {

    [Header("Cook Times")]
    public int TimeToCook;
    public int TimeToBurn;

    float CookTime;
    float BurnTime;

    [Header("Animation Controller")]
    public Animator Controller;

    [Header("Animation State Names")]
    public string CookingStateName;
    public string BurningStateName;

    string PreviousState;

	// Use this for initialization
	void Start () {
        CookTime = TimeToCook;
        BurnTime = TimeToBurn;

        PreviousState = "";
	}
	
	// Update is called once per frame
    void Update () {

	}
}
