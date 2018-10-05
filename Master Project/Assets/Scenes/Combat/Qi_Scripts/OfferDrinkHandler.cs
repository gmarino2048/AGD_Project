using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferDrinkHandler : MonoBehaviour {

    public ManagerBar bar;
    public MonsterAction.Monster monster;

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
    } 
}
