using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

namespace Dialogue
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Nessie Assets")]
        public string NessieName;
        public NessieController Nessie;

        [Header("Cerberus Assets")]
        public string CerberusName;
        public CerberusController Cerberus;

        [Header("[REDACTED] Assets")]
        public string RedactedName;
        public RedactedController Redacted;

        [Header("Dialogue Controller")]
        public DialogueManager Manager;

        void Awake()
        {
            GameNarrativeManager narrativeManager = FindObjectOfType<GameNarrativeManager>();
            MonsterFactory monsterFactory = FindObjectOfType<MonsterFactory>();

            try
            {
                Guid monster = narrativeManager.CurrentStage.MonsterID;
                string monsterName = monsterFactory.LoadMonster(monster).Name;

                if (monsterName == NessieName) SetNessie();
                else if (monsterName == CerberusName) SetCerberus();
                else if (monsterName == RedactedName) SetRedacted();
                else
                {
                    Debug.LogWarning("Unexpected value -- Defaulting to Nessie");
                    SetNessie();
                }
            }
            catch
            {
                Debug.LogWarning("Dialogue not running in scene -- Defaulting to Nessie");
                SetNessie();
            }
        }

        void SetNessie ()
        {
            Nessie.gameObject.SetActive(true);
            Manager.Patron = Nessie;

            Cerberus.gameObject.SetActive(false);
            Redacted.gameObject.SetActive(false);
        }

        void SetCerberus()
        {
            Cerberus.gameObject.SetActive(true);
            Manager.Patron = Cerberus;

            Nessie.gameObject.SetActive(false);
            Redacted.gameObject.SetActive(false);
        }

        void SetRedacted()
        {
            Redacted.gameObject.SetActive(true);
            Manager.Patron = Redacted;

            Nessie.gameObject.SetActive(false);
            Cerberus.gameObject.SetActive(false);
        }
    }
}