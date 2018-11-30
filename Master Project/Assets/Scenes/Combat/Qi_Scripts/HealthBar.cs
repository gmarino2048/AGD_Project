using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Combat
{
    public class HealthBar : MonoBehaviour
    {
        private MonsterAction ma;
        private float LastHealth;
        private float Health;
        private float t;
        private bool Changed;
        public Transform healthbar;
        public Slider healthFill;
        public Text healtnum;
        public int currentHealth;
        public int maxHealth;


        private void Awake()
        {
            ma = GameObject.Find("Monster").GetComponent<MonsterAction>();
            currentHealth = 100;
            maxHealth = 100;
            healtnum.text = "Health: " + (healthFill.value * 100f).ToString();
            healthFill.value = (float)currentHealth / maxHealth;
            Changed = false;
        }
        public void Update()
        {
            if (currentHealth <= 0)
            {
                ma.Combat = false;
            }
            if (Changed)
            {
                Health = Mathf.Lerp(LastHealth, currentHealth, t);
                Health = (float)Math.Round(Health, 0);
                //Debug.Log(Health);
                healthFill.value = Health / maxHealth;
                t += Time.deltaTime * 2f;
            }
            if(Health == currentHealth)
            {
                Changed = false;
            }
        }
        public void ChangeHealth(int amount)
        {
            LastHealth = currentHealth;
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            t = 0f;
            Changed = true;
        }
        public void DisplayHealthNum()
        {
            healtnum.text = "Health: " + (healthFill.value * 100f).ToString();
        }
        public float GetCurrentHealthValue()
        {
            return healthFill.value * 100;
        }
    }
}
