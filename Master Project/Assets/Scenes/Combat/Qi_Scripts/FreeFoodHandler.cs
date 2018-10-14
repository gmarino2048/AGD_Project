using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeFoodHandler : MonoBehaviour {

    private MonsterAction ma;

    public ManagerBar bar;
    public MonsterAction.Monster monster;

    private void Awake()
    {
        ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
    }
    public void FreeFood()
    {
        Debug.Log("FreeFood");
        if(monster == MonsterAction.Monster.Nessie)
        {
            bar.ChangeManagerBar(-10);
        }
        if (monster == MonsterAction.Monster.Cerberus)
        {
            bar.ChangeManagerBar(-25);
        }
        if (monster == MonsterAction.Monster.REDACTED)
        {
            bar.ChangeManagerBar(-5);
        }
        ma.playermoved = true;
    }
}
