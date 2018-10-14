using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CouponHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        public MonsterAction.Monster monster;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void Coupon()
        {
            Debug.Log("Coupon");
            if (monster == MonsterAction.Monster.Nessie)
            {
                bar.ChangeManagerBar(-15);
            }
            if (monster == MonsterAction.Monster.Cerberus)
            {
                bar.ChangeManagerBar(-5);
            }
            if (monster == MonsterAction.Monster.REDACTED)
            {
                bar.ChangeManagerBar(-10);
            }
            ma.PlayerMoved = true;
        }
    }
}
