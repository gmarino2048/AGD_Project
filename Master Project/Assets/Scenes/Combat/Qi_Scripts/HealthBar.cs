using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private MonsterAction ma;

    public Transform healthbar;
    public Slider healthFill;
    public Text healtnum;
    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        currentHealth = 100;
        maxHealth = 100;
        healtnum.text = (healthFill.value * 100f).ToString();
        healthFill.value = (float)currentHealth / maxHealth;
    }
    public void Update()
    {
        if(currentHealth <= 0)
        {
            ma.combat = false;
        }
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthFill.value = (float)currentHealth / maxHealth;
    }
    public void DisplayHealthNum()
    {
        healtnum.text = (healthFill.value * 100f).ToString();
    }
}
