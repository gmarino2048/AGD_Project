using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class Heal : MonoBehaviour
    {
        private MonsterAction ma;
        private Image col;
        public GameObject HealHider;
        public HealthBar healthbar;
        private int healtime;
        public Text time;

        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
            healtime = 4;
            HealHider.SetActive(false);
        }
        private void Update()
        {
            time.text =  "(" + healtime.ToString() + ")";
        }
        public void HealHandler()
        {
            //Output this to console when the Button is clicked
            if (healtime >= 0)
            {
                if(healtime > 0)
                {
                    ma.PlayerHealed = true;
                    healthbar.ChangeHealth(25);
                    healtime--;
                }
                if (healtime == 0)
                {
                    HealHider.SetActive(true);
                }
            }
        }
    }
}
