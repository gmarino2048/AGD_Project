using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; // needed for Regex

public class InputTextScript : MonoBehaviour {

    private string text;
    
     

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        text = Regex.Replace(text, @"[^a-zA-Z0-9 ]", "");
    }
}
