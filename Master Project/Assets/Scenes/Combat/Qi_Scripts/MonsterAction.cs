﻿using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private AudioController Ad;
        private GameObject Nessie;
        private GameObject Cerberus;
        private GameObject REDACTED;
        private MonsterNames Names = new MonsterNames();

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

        private string _GAME_OVER_SCENE_NAME = "Game OVer";
        private string _DIALOGUE_SCENE_NAME = "DialogueScene";
        private string _MONOLOGUE_SCENE_NAME = "Monologue";

        void Awake()
        {
            monstermoved = false;
            PlayerMoved = false;
            PlayerHealed = false;
            Win = false;
            Combat = true;
            newRound = true;
            Ih = GameObject.Find("InfoHider");
            Tk = GameObject.Find("Talk").GetComponent<Talk>();
            Ad = GameObject.Find("Monster").GetComponent<AudioController>();
            if (GameObject.Find(Names._NESSIE_NAME))
            {
                Nessie = GameObject.Find(Names._NESSIE_NAME);
            }
            else
            {
                Debug.Log("Cant Find " + Names._NESSIE_NAME);
            }

            if (GameObject.Find(Names._CERBERUS_NAME))
            {
                Cerberus = GameObject.Find(Names._CERBERUS_NAME);
            }
            else
            {
                Debug.Log("Cant Find " + Names._CERBERUS_NAME);
            }
            if (GameObject.Find(Names._REDACTED_NAME))
            {
                REDACTED = GameObject.Find(Names._REDACTED_NAME);
            }
            else
            {
                Debug.Log("Cant Find " + Names._REDACTED_NAME);
            }
        }
        void Start()
        {
            _CombatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
            if (_CombatInitiator != null)
            {
                var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
                CurrentMonster = monsterFactory.LoadMonster(_CombatInitiator.MonsterID);
                if(CurrentMonster.ToString() == Names._NESSIE_NAME)
                {
                    Nessie.SetActive(true);
                    Cerberus.SetActive(false);
                    REDACTED.SetActive(false);
                }
                else if (CurrentMonster.ToString() == Names._CERBERUS_NAME)
                {
                    Nessie.SetActive(false);
                    Cerberus.SetActive(true);
                    REDACTED.SetActive(false);
                }
                else if (CurrentMonster.ToString() == Names._REDACTED_NAME)
                {
                    Nessie.SetActive(false);
                    Cerberus.SetActive(false);
                    REDACTED.SetActive(true);
                }
                Movement = GameObject.Find(CurrentMonster.ToString()).GetComponent<Animator>();
                Bar.SetValue((int)Mathf.Ceil(_CombatInitiator.InitialManagerMeterValue * Bar.maxManagerValue));
            }
            else
            {
                //intial conbatchoice and its decay rates
                var combatChoices = new Dictionary<CombatChoice, CombatChoiceStatus>();
                combatChoices.Add(CombatChoice.Beg, new CombatChoiceStatus(35, 0f, 35));
                combatChoices.Add(CombatChoice.Coupon, new CombatChoiceStatus(15, 0.85f, 5));
                combatChoices.Add(CombatChoice.Flatter, new CombatChoiceStatus(-5, 1.5f, -15));
                combatChoices.Add(CombatChoice.FreeFood, new CombatChoiceStatus(10, 0.75f, 3));
                combatChoices.Add(CombatChoice.OfferDrink, new CombatChoiceStatus(25, 0.80f, 10));
                combatChoices.Add(CombatChoice.Reason, new CombatChoiceStatus(10, 0.9f, 2));
                // Just default, other values are not necessary for this scene.
                CurrentMonster = new MonsterData("[REDACTED]", 0, null, combatChoices);
                Debug.Log(CurrentMonster.ToString());
                if (CurrentMonster.ToString() == Names._NESSIE_NAME)
                {
                    Nessie.SetActive(true);
                    Cerberus.SetActive(false);
                    REDACTED.SetActive(false);
                }
                if (CurrentMonster.ToString() == Names._CERBERUS_NAME)
                {
                    Nessie.SetActive(false);
                    Cerberus.SetActive(true);
                    REDACTED.SetActive(false);
                }
                if (CurrentMonster.ToString() == Names._REDACTED_NAME)
                {
                    Nessie.SetActive(false);
                    Cerberus.SetActive(false);
                    REDACTED.SetActive(true);
                }
                Movement = GameObject.Find(CurrentMonster.ToString()).GetComponent<Animator>();
            }
            StartCoroutine("CombatFunction");
            
        }
        
        void Update()
        {
            
        }

        IEnumerator CombatFunction()
        {
            while (Combat)
            {
                Ad.MMC();
                Ad.PlayerHPL();
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

            if (Win)
            {
                CombatMessage.text = "Victory!";
                Ad.MMHZ();
                yield return new WaitForSeconds(1f);
                var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
                if (gameNarrativeManager == null)
                {
                    SceneManager.LoadScene(_DIALOGUE_SCENE_NAME);
                }

                if (gameNarrativeManager.AnyStagesLeft())
                {
                    SceneManager.LoadScene(_DIALOGUE_SCENE_NAME, LoadSceneMode.Single);
                }
                else {
                    SceneManager.LoadScene(_MONOLOGUE_SCENE_NAME, LoadSceneMode.Single);
                }
            }
            else
            {
                SceneManager.LoadScene(_GAME_OVER_SCENE_NAME, LoadSceneMode.Single);
            }
        }
        IEnumerator Monsterdamamged()
        {
            CombatMessage.text = CurrentMonster + " Received Damage";
            Ad.MH();
            Movement.SetTrigger("hit");
            //Should we also play the ManagerMeterGoesDown clip after monster hit?
            yield return new WaitForSeconds(0.5f);
            Ad.MMGD();
            yield return null;
        }
        IEnumerator WrongChoice()
        {
            CombatMessage.text = "Crap! I Made a Wrong Chioce!";
            Ad.MMGU();
            yield return null;
        }
        IEnumerator Playerheal()
        {
            CombatMessage.text = "You Feel Better";
            Ad.PHPGU();
            yield return null;
        }
        IEnumerator MonsterAttack()
        {
            monsteraction = monsterdice.Next(1, 6);
            if (monsteraction == 1 || monsteraction == 3 || monsteraction == 5)
            {
                damageholder = damagedice.Next(0, 5);
                health.ChangeHealth(-15 - damageholder);
                CombatMessage.text = CurrentMonster + "'s Attack! Dealt " + (15 + damageholder).ToString() + " Damage!";
                Movement.SetTrigger("attack3");
                Ad.PH();
                yield return new WaitForSeconds(0.5f);
                Ad.PHPGD();
            }
            else if (monsteraction == 2 || monsteraction == 4)
            {
                Bar.IncrementValue(10);
                CombatMessage.text = CurrentMonster + "used Healing! Manager Meter increased 10!";
                Movement.SetTrigger("attack2");
                Ad.MMGU();
            }
            else
            {
                damageholder = damagedice.Next(0, 10);
                health.ChangeHealth(-25 - damageholder);
                CombatMessage.text ="Critical! " + CurrentMonster + " Dealt " + (25 + damageholder).ToString() + " Damage!";
                Movement.SetTrigger("attack1");
                Ad.PH();
                yield return new WaitForSeconds(0.5f);
                Ad.PHPGD();
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
            Ad.MMGU();
            newRound = true;
            Tk.showup = false;
            yield return null;

        }
    }
}
