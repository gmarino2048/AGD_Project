using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class OfferDrinkHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void OfferDrink()
        {
            Debug.Log("Offer Drink");
            bar.IncrementValue(-25); //TODO: Rewrite for handling varying degrees of success
            ma.PlayerMoved = true;
        }
    }
}
