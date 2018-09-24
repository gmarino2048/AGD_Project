using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBarNumberDisplay : MonoBehaviour {

    public Image Managerbar;
    public Text Numberdisplay;
    private float Number;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Number = Managerbar.fillAmount * 100;
        Numberdisplay.text = Number.ToString() + "%";
	}
}
