﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour {

    public GameObject heal;
    public HealthBar healthbar;
    private int healtime;

    private void Awake()
    {
        healtime = 4;
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
    }
}
