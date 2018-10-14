using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatterHandler : MonoBehaviour {
    private MonsterAction ma;
    public ManagerBar bar;
    public MonsterAction.Monster monster;
    private void Awake()
    {
        ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
    }
    public void Flatter(){
        Debug.Log("Flatter");
        if (monster == MonsterAction.Monster.Nessie)
        {
            bar.ChangeManagerBar(-5);
        }
        if (monster == MonsterAction.Monster.Cerberus)
        {
            bar.ChangeManagerBar(-15);
        }
        if (monster == MonsterAction.Monster.REDACTED)
        {
            bar.ChangeManagerBar(-25);
        }
        ma.playermoved = true;
    }
}
