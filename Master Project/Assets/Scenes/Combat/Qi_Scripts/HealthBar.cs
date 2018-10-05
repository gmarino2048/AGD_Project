using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Transform healthbar;
    public Slider healthFill;

    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        currentHealth = 10;
        maxHealth = 100;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthFill.value = (float)currentHealth / maxHealth;
    }
}
