using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBar : MonoBehaviour {

    public Transform managerbar;
    public Slider baramount;
    public int currentManagerValue;
    public int maxManagerValue;
    public Text managernum;
    private MonsterAction ma;

	// Use this for initialization
	void Awake() {
        baramount.value = 1f;
        ma = GameObject.Find("MonsterSprite").GetComponent<MonsterAction>();
        baramount.value = (float)currentManagerValue / maxManagerValue;
        managernum.text = (baramount.value * 100f).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeManagerBar(int value)
    {
        currentManagerValue += value;
        currentManagerValue = Mathf.Clamp(currentManagerValue, 0, maxManagerValue);
        baramount.value = (float)currentManagerValue / maxManagerValue;
        ma.MonsterHitted();
    }
    public void DisplayManagerNum()
    {
        managernum.text = (baramount.value * 100f).ToString();
    }
}
