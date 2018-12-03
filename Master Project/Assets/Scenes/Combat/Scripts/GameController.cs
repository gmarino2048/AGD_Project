using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class GameController : MonoBehaviour {

        [Header("UI Bars")]
        public InfoBarController HealthBar;
        public InfoBarController ManagerBar;

        [Header("Game Controls")]
        public PlayByPlayController PlayByPlay;
        public SFXController SFX;
        public CombatReactionController CombatOverlays;

        [Header("Reaction Controls")]
        public Animator Monster;
        public string AttackTrigger = "attack1";
        public string DamageTrigger = "hit";

        [Header("Health Parameters")]
        public bool HealthActive;
        public Button HealthButton;
        public Image HealthCover;

        [Header("Beg Parameters")]
        public bool BegActive;
        public Button BegButton;
        public Image BegCover;

        public enum LastEvent
        {
            UserAttack,
            MonsterAttack,
            UserHeal,
            MonsterHeal,
            RoundComplete
        }

        private void Start()
        {
            HealthActive = true;
            BegActive = true;

            HealthCover.gameObject.SetActive(false);

        }

        private void Update()
        {
            if (HealthBar.Percentage < 20 || ManagerBar.Percentage > 80)
            {
                SetBegActive(true, true);
            }
            else
            {
                SetBegActive(false, true);
            }
        }

        public void HealPlayer (float amount)
        {
            HealthBar.SetValue(HealthBar.Percentage + amount);

            CombatOverlays.PlayHeal();
            StartCoroutine(SFX.PlayerHeal());
        }

        public void DamagePlayer (float amount)
        {
            if (amount < 0)
            {
                HealPlayer(amount * -1);
            }
            else
            {
                HealthBar.SetValue(HealthBar.Percentage - amount);
                Monster.SetTrigger(AttackTrigger);

                CombatOverlays.PlayDamage();
                StartCoroutine(SFX.PlayerIsHit());
            }
        }

        public void HealMonster (float amount)
        {
            ManagerBar.SetValue(ManagerBar.Percentage + amount);

            StartCoroutine(SFX.MonsterHeal());
        }

        public void DamageMonster (float amount)
        {
            if (amount < 0)
            {
                HealMonster(amount * -1);
            }
            else
            {
                ManagerBar.SetValue(ManagerBar.Percentage - amount);
                Monster.SetTrigger(DamageTrigger);

                StartCoroutine(SFX.MonsterIsHit());
            }
        }

        public void Miss ()
        {
            Monster.SetTrigger(AttackTrigger);
            SFX.MonsterMisses();
        }

        public void SetHealActive (bool active)
        {
            if (active)
            {
                if (HealthActive)
                {
                    HealthButton.gameObject.SetActive(true);
                    HealthCover.gameObject.SetActive(false);
                }
            }
            else
            {
                HealthButton.gameObject.SetActive(false);
                HealthCover.gameObject.SetActive(true);
                HealthActive = false;
            }
        }

        public void SetBegActive(bool active, bool hide)
        {
            if (active)
            {
                if (BegActive)
                {
                    BegCover.gameObject.SetActive(false);
                    BegButton.gameObject.SetActive(true);
                }
            }
            else
            {
                BegCover.gameObject.SetActive(true);
                BegButton.gameObject.SetActive(false);

                if (BegActive) BegActive = hide;
            }
        }
    }
}