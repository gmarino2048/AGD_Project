using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponHandler : MonoBehaviour {

    public ManagerBar bar;
    public MonsterAction.Monster monster;

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
    }
}
