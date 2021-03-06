﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class OfferDrinkHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        private void Awake()
        {
            ma = GameObject.Find("Monster").GetComponent<MonsterAction>();
        }
        public void OfferDrink()
        {
            bar.HandlePlayerCombatChoice(CombatChoice.OfferDrink);
            ma.PlayerMoved = true;
        }
    }
}
