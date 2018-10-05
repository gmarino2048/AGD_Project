using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBar : MonoBehaviour {

    public Transform managerbar;
    public Slider baramount;
    public int currentManagerValue;
    public int maxManagerValue;

	// Use this for initialization
	void Awake() {
        baramount.value = 1f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeManagerBar(int value)
    {
        currentManagerValue += value;
        currentManagerValue = Mathf.Clamp(currentManagerValue, 0, maxManagerValue);
        baramount.value = (float)currentManagerValue / maxManagerValue;
    }
}
