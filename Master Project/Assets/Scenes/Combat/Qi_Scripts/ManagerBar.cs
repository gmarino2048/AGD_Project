using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBar : MonoBehaviour {
    private MonsterAction ma;
    public Transform managerbar;
    public Slider baramount;
    public int currentManagerValue;
    public int maxManagerValue;
    public Text managernum;

	// Use this for initialization
	void Awake() {
        ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        currentManagerValue = 50;
        maxManagerValue = 100;
        baramount.value = (float)currentManagerValue / maxManagerValue;
        managernum.text = (baramount.value * 100f).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		if(currentManagerValue >= maxManagerValue)
        {
            ma.combat = false;
        }
        if(currentManagerValue <= 0)
        {
            ma.combat = false;
            ma.win = true;
        }

    }
    public void ChangeManagerBar(int value)
    {
        currentManagerValue += value;
        currentManagerValue = Mathf.Clamp(currentManagerValue, 0, maxManagerValue);
        baramount.value = (float)currentManagerValue / maxManagerValue;

    }
    public void DisplayManagerNum()
    {
        managernum.text = (baramount.value * 100f).ToString();
    }
}
