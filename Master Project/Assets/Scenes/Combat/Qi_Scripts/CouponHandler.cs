using Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CouponHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void Coupon()
        {
            Debug.Log("Coupon");
            bar.IncrementValue(-15); //TODO: Rewrite for handling varying degrees of success
            ma.PlayerMoved = true;
        }
    }
}
