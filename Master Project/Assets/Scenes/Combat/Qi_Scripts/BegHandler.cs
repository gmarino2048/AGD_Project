﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class BegHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        public GameObject beg;
        public bool begshown;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
            beg.SetActive(false);
            begshown = false;
        }

        public void Beg()
        {
            bar.HandlePlayerCombatChoice(CombatChoice.Beg);
            begshown = true;
            beg.SetActive(false);
            ma.PlayerMoved = true;
        }
    }
}
