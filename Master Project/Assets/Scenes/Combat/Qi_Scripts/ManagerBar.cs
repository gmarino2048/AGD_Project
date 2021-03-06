﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monsters;
using System;

namespace Combat
{
    public class ManagerBar : MonoBehaviour
    {
        private MonsterAction ma;
        private float LastManagerBar;
        private float Manager;
        private float t;
        private bool Changed;
        public Transform managerbar;
        public Slider baramount;
        public int currentManagerValue;
        public int maxManagerValue;
        public Text managernum;

        // Use this for initialization
        void Awake()
        {
            ma = GameObject.Find("Monster").GetComponent<MonsterAction>();
            //currentManagerValue = 75;
            maxManagerValue = 100;
            baramount.value = (float)currentManagerValue / maxManagerValue;
            managernum.text = "Manager Meter: " + (baramount.value * 100f).ToString();
            Changed = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentManagerValue >= maxManagerValue)
            {
                ma.Combat = false;
            }
            if (currentManagerValue <= 0)
            {
                ma.Combat = false;
                ma.Win = true;
            }
            if (Changed)
            {
                Manager = Mathf.Lerp(LastManagerBar, currentManagerValue, t);
                Manager = (float)Math.Round(Manager, 0);
                //Debug.Log(Manager);
                baramount.value = Manager/ maxManagerValue;
                t += 0.1f;
            }
            if (Manager == currentManagerValue)
            {
                Changed = false;
            }

        }

        /// <summary>
        /// Sets the value of the manager bar
        /// </summary>
        /// <param name="value">The value to be set to</param>
        public void SetValue(float value)
        {
            baramount.value = (float) value / maxManagerValue;
        }

        /// <summary>
        /// Increments the value of the manager bar by the given offset
        /// </summary>
        /// <param name="offset">The amount to add to the current manager bar value</param>
        public void IncrementValue(int offset)
        {
            LastManagerBar = currentManagerValue;
            currentManagerValue += offset;
            currentManagerValue = Mathf.Clamp(currentManagerValue, 0, maxManagerValue);
            //baramount.value = (float)currentManagerValue / maxManagerValue;
            t = 0f;
            Changed = true;
        }

        /// <summary>
        /// Updates the manager bar value based on the option the player chose
        /// </summary>
        /// <param name="combatChoice">The enum representing the combat choice the player decided upon</param>
        public void HandlePlayerCombatChoice(CombatChoice combatChoice)
        {
            // In the case of just testing this one scene in isolation
            if (ma.CurrentMonster == null || ma.CurrentMonster.CombatChoices == null || !ma.CurrentMonster.CombatChoices.ContainsKey(combatChoice))
            {
                IncrementValue(-10);
                return;
            }
            foreach(KeyValuePair<CombatChoice, CombatChoiceStatus> option in ma.CurrentMonster.CombatChoices)
            {
                if(option.Key == combatChoice)
                {
                    IncrementValue(-1 * option.Value.Power);
                }
                option.Value.UpdatePower();
            }
        }

        /// <summary>
        /// Displays the current manager bar value
        /// </summary>
        public void DisplayManagerNum()
        {
            managernum.text = "Manager Meter: " + (baramount.value * 100f).ToString();
        }

        public float GetCurrentBarValue()
        {
            return currentManagerValue;
        }
    }
}
