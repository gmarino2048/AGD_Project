using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class OfferDrinkHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        public MonsterAction.Monster monster;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void OfferDrink()
        {
            Debug.Log("Offer Drink");
            if (monster == MonsterAction.Monster.Nessie)
            {
                bar.ChangeManagerBar(-25);
            }
            if (monster == MonsterAction.Monster.Cerberus)
            {
                bar.ChangeManagerBar(-10);
            }
            if (monster == MonsterAction.Monster.REDACTED)
            {
                bar.ChangeManagerBar(-15);
            }
            ma.PlayerMoved = true;
        }
    }
}
