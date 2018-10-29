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

        void Awake()
        {
            Movement = GetComponent<Animator>();
            monstermoved = false;
            PlayerMoved = false;
            PlayerHealed = false;
            Win = false;
            Combat = true;
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
                // Just default, other values are not necessary for this scene.
                CurrentMonster = new MonsterData("Nessie", 0, null, null);
            }
            StartCoroutine("CombatFunction");
        }

        IEnumerator CombatFunction()
        {
            while (Combat)
            {
                //Debug.Log(Combat);
                if (!monstermoved && !PlayerMoved)
                {
                    CombatMessage.text = "What should I do?";
                    CombatUI.SetActive(true);
                }
                if (!monstermoved && (PlayerMoved || PlayerHealed))
                {
                    CombatUI.SetActive(false);
                    if (PlayerMoved)
                    {
                        StartCoroutine("Monsterdamamged");
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
            CombatMessage.text = CurrentMonster + " received damage";
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
                CombatMessage.text = CurrentMonster + " used Normal Attack! Dealt " + (15 + damageholder).ToString() + " damage!";
                Movement.SetTrigger("attack3");

            }
            else if (monsteraction == 2 || monsteraction == 4)
            {
                Bar.IncrementValue(10);
                CombatMessage.text = CurrentMonster + " used Healing! Raised manager meter by 10!";
                Movement.SetTrigger("attack2");
            }
            else
            {
                damageholder = damagedice.Next(0, 10);
                health.ChangeHealth(-25 - damageholder);
                CombatMessage.text = CurrentMonster + " used Super Attack! Dealt " + (25 + damageholder).ToString() + " damage!";
                Movement.SetTrigger("attack1");
            }
            monstermoved = true;
            yield return null;
        }

        IEnumerator NewTurn()
        {
            CombatMessage.text = "Too loud! Manager bar goes up!";
            monstermoved = false;
            PlayerMoved = false;
            PlayerHealed = false;
            Bar.IncrementValue(5);
            yield return null;

        }
    }
}
