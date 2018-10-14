using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour {
    private MonsterAction ma;
    public GameObject heal;
    public HealthBar healthbar;
    private int healtime;
    public Text time;

    private void Awake()
    {
        ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        healtime = 4;
    }
    private void Update()
    {
        time.text = "Heal" + "(" + healtime.ToString() + ")";
    }
    public void HealHandler()
    {
        //Output this to console when the Button is clicked
        Debug.Log("Heal");
        if(healtime >= 0)
        {
            healthbar.ChangeHealth(25);
            healtime--;
            if(healtime == 0)
            {
                heal.SetActive(false);
            }
        }
        else
        {
            heal.SetActive(false);
        }
        ma.PlayerHealed = true;
    }
}
