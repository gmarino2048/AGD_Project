using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class GameController : MonoBehaviour {

        [Header("UI Bars")]
        public InfoBarController HealthBar;
        public InfoBarController ManagerBar;

        [Header("Game Controls")]
        public PlayByPlayController PlayByPlay;
        public SFXController SFX;

        [Header("Reaction Controls")]
        public Animator Monster;
        public string AttackTrigger = "attack1";
        public string DamageTrigger = "hit";

        public enum LastEvent
        {
            UserAttack,
            MonsterAttack,
            UserHeal,
            MonsterHeal,
            RoundComplete
        }

        public void HealPlayer (float amount)
        {
            HealthBar.SetValue(HealthBar.Percentage + amount);

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

                StartCoroutine(SFX.PlayerIsHit());
            }
        }

        public void HealMonster (float amount)
        {
            ManagerBar.SetValue(ManagerBar.Percentage + amount);
            Monster.SetTrigger(AttackTrigger);

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

        public void SetBegActive (bool active)
        {

        }

        public void SetHealActive (bool active)
        {

        }
    }
}