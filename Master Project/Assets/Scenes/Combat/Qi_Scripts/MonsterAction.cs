using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MonsterAction : MonoBehaviour {

    System.Random monsterdice = new System.Random();
    System.Random damagedice = new System.Random();
    private int monsteraction;
    private bool monstermoved;
    private int damageholder;

    public enum Monster { Nessie, Cerberus, REDACTED };
    public Monster monster;
    public Animator Movement;
    public ManagerBar bar;
    public HealthBar health;
    public bool playermoved;
    public bool playerhealed;
    public bool combat;
    public bool win;
    public Text CombatMessage;
    public GameObject CombatUI;


    private void Awake()
    {
        monstermoved = false;
        playermoved = false;
        playerhealed = false;
        win = false;
        combat = true;
        monster = Monster.Nessie;
    }
    private void Start()
    {
        StartCoroutine("Combat");
    }

    IEnumerator Combat()
    {
        while (combat)
        {
            Debug.Log(combat);
            if (!monstermoved && !playermoved)
            {
                CombatMessage.text = "What should I do?";
                CombatUI.SetActive(true);
            }
            if (!monstermoved && (playermoved || playerhealed))
            {
                CombatUI.SetActive(false);
                if (playermoved)
                {
                    StartCoroutine("Monsterdamamged");
                }
                yield return new WaitForSeconds(2f);
                StartCoroutine("MonsterAttack");
                yield return new WaitForSeconds(2f);
                if (!combat)
                {
                    break;
                }
            }
            if (monstermoved && (playermoved || playerhealed))
            {
                StartCoroutine("NewTurn");
                yield return new WaitForSeconds(2f);
            }
            yield return null;
        }
        if (!win)
        {
            CombatMessage.text = "Game Over!";
        }
        else
        {
            CombatMessage.text = "Victory!";
        }

    }
    IEnumerator Monsterdamamged()
    {
        CombatMessage.text = monster + " received damage";
        Movement.SetTrigger("hit");
        yield return null;
    }
    IEnumerator MonsterAttack()
    {
        monsteraction = monsterdice.Next(1, 6);
        if (monsteraction == 1 || monsteraction == 3 || monsteraction == 5)
        {
            damageholder = damagedice.Next(0, 5);
            health.ChangeHealth(-15 - damageholder);
            CombatMessage.text = monster + " used Normal Attack! Dealt " + (15 + damageholder).ToString()+ " damage!";
            Movement.SetTrigger("attack3");
            
        }
        else if (monsteraction == 2 || monsteraction == 4)
        {
            bar.ChangeManagerBar(10);
            CombatMessage.text = monster + " used Healing! Raised manager meter by 10!";
            Movement.SetTrigger("attack2");
        }
        else
        {
            damageholder = damagedice.Next(0, 10);
            health.ChangeHealth(-25 - damageholder);
            CombatMessage.text = monster + " used Super Attack! Dealt " + (25 + damageholder).ToString() + " damage!";
            Movement.SetTrigger("attack1");
        }
        monstermoved = true;
        yield return null;
    }

    IEnumerator NewTurn()
    {
        CombatMessage.text = "Too loud! Manager bar goes up!";
        monstermoved = false;
        playermoved = false;
        playerhealed = false;
        bar.ChangeManagerBar(5);
        yield return null;

    }
}
