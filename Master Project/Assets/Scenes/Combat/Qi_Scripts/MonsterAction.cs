using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class MonsterAction : MonoBehaviour
    {

        System.Random monsterdice = new System.Random();
        System.Random damagedice = new System.Random();
        private int monsteraction;
        private bool monstermoved;
        private int damageholder;
        private bool newRound;
        private float CurrentBarValue;
        private float LastBarValue;
        private GameObject Ih;
        private Talk Tk;

        public Animator Movement;
        public ManagerBar Bar;
        public HealthBar health;
        public bool PlayerMoved { get; set; }
        public bool PlayerHealed { get; set; }
        public bool Combat { get; set; }
        public bool Win { get; set; }
        public Text CombatMessage;
        public GameObject CombatUI;

        private CombatInitiator _CombatInitiator;
        public MonsterData CurrentMonster;

        //private readonly Guid _NESSIE_GUID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");
       

        void Awake()
        {
            Movement = GetComponent<Animator>();
            monstermoved = false;
            PlayerMoved = false;
            PlayerHealed = false;
            Win = false;
            Combat = true;
            newRound = true;
            Ih = GameObject.Find("InfoHider");
            Tk = GameObject.Find("Talk").GetComponent<Talk>();
        }
        void Start()
        {
            _CombatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
            if (_CombatInitiator != null)
            {
                var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
                CurrentMonster = monsterFactory.LoadMonster(_CombatInitiator.MonsterID);
                Bar.SetValue((int)Mathf.Ceil(_CombatInitiator.InitialManagerMeterValue * Bar.maxManagerValue));
            }
            else
            {
                //intial conbatchoice and its decay rates
                var combatChoices = new Dictionary<CombatChoice, CombatChoiceStatus>();
                combatChoices.Add(CombatChoice.Beg, new CombatChoiceStatus(35, 0f, 0));
                combatChoices.Add(CombatChoice.Coupon, new CombatChoiceStatus(15, 0.85f, 5));
                combatChoices.Add(CombatChoice.Flatter, new CombatChoiceStatus(-5, 1.5f, -15));
                combatChoices.Add(CombatChoice.FreeFood, new CombatChoiceStatus(10, 0.75f, 3));
                combatChoices.Add(CombatChoice.OfferDrink, new CombatChoiceStatus(25, 0.80f, 10));
                combatChoices.Add(CombatChoice.Reason, new CombatChoiceStatus(10, 0.9f, 2));
                // Just default, other values are not necessary for this scene.
                CurrentMonster = new MonsterData("Nessie", 0, null, combatChoices);
            }
            StartCoroutine("CombatFunction");
        }

        IEnumerator CombatFunction()
        {
            while (Combat)
            {
                if (newRound)
                {
                    LastBarValue = Bar.GetCurrentBarValue();
                    newRound = false;
                }
                if (!monstermoved && !PlayerMoved)
                {
                    CombatMessage.text = "What should I do?";
                    CombatUI.SetActive(true);
                    if (!Tk.showup)
                    {
                        Ih.SetActive(true);
                        Tk.showup = true;
                    }
                }
                if (!monstermoved && (PlayerMoved || PlayerHealed))
                {
                    CurrentBarValue = Bar.GetCurrentBarValue();
                    CombatUI.SetActive(false);
                    if (PlayerMoved && (CurrentBarValue < LastBarValue))
                    {
                        StartCoroutine("Monsterdamamged");
                    }
                    if (PlayerMoved && (CurrentBarValue > LastBarValue))
                    {
                        StartCoroutine("WrongChoice");
                    }
                    else if (PlayerHealed)
                    {
                        StartCoroutine("Playerheal");
                    }
                    yield return new WaitForSeconds(2f);
                    StartCoroutine("MonsterAttack");
                    yield return new WaitForSeconds(2f);
                    if (!Combat)
                    {
                        break;
                    }
                }
                if (monstermoved && (PlayerMoved || PlayerHealed))
                {
                    StartCoroutine("NewTurn");
                    yield return new WaitForSeconds(2f);
                }
                yield return null;
            }
            if (!Win)
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
            CombatMessage.text = CurrentMonster + " Received Damage";
            Movement.SetTrigger("hit");
            yield return null;
        }
        IEnumerator WrongChoice()
        {
            CombatMessage.text = "Crap! I Made a Wrong Chioce!";
            yield return null;
        }
        IEnumerator Playerheal()
        {
            CombatMessage.text = "You Feel Better";
            yield return null;
        }
        IEnumerator MonsterAttack()
        {
            monsteraction = monsterdice.Next(1, 6);
            if (monsteraction == 1 || monsteraction == 3 || monsteraction == 5)
            {
                damageholder = damagedice.Next(0, 5);
                health.ChangeHealth(-15 - damageholder);
                CombatMessage.text = CurrentMonster + " Used Attack! Dealt " + (15 + damageholder).ToString() + " Damage!";
                Movement.SetTrigger("attack3");

            }
            else if (monsteraction == 2 || monsteraction == 4)
            {
                Bar.IncrementValue(10);
                CombatMessage.text = CurrentMonster + " Used Healing! Raised Manager Meter By 10!";
                Movement.SetTrigger("attack2");
            }
            else
            {
                damageholder = damagedice.Next(0, 10);
                health.ChangeHealth(-25 - damageholder);
                CombatMessage.text = CurrentMonster + " Used Super Attack! Dealt " + (25 + damageholder).ToString() + " Damage!";
                Movement.SetTrigger("attack1");
            }
            monstermoved = true;
            yield return null;
        }

        IEnumerator NewTurn()
        {
            CombatMessage.text = "Too Loud! Manager Bar Goes Up!";
            monstermoved = false;
            PlayerMoved = false;
            PlayerHealed = false;
            Bar.IncrementValue(5);
            newRound = true;
            Tk.showup = false;
            yield return null;

        }
    }
}
