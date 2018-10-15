using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class ManagerBar : MonoBehaviour
    {
        private MonsterAction ma;
        public Transform managerbar;
        public Slider baramount;
        public int currentManagerValue;
        public int maxManagerValue;
        public Text managernum;

        // Use this for initialization
        void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
            currentManagerValue = 50;
            maxManagerValue = 100;
            baramount.value = (float)currentManagerValue / maxManagerValue;
            managernum.text = (baramount.value * 100f).ToString();
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

        }

        /// <summary>
        /// Sets the value of the manager bar
        /// </summary>
        /// <param name="value">The value to be set to</param>
        public void SetValue(int value)
        {
            baramount.value = (float) value / maxManagerValue;
        }

        /// <summary>
        /// Increments the value of the manager bar by the given offset
        /// </summary>
        /// <param name="offset">The amount to add to the current manager bar value</param>
        public void IncrementValue(int offset)
        {
            currentManagerValue += offset;
            currentManagerValue = Mathf.Clamp(currentManagerValue, 0, maxManagerValue);
            baramount.value = (float)currentManagerValue / maxManagerValue;
        }

        /// <summary>
        /// Displays the current manager bar value
        /// </summary>
        public void DisplayManagerNum()
        {
            managernum.text = (baramount.value * 100f).ToString();
        }
    }
}
