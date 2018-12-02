using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class GameController : MonoBehaviour {

        [Header("UI Bars")]
        public InfoBarController HealthBar;
        public InfoBarController ManagerBar;

        [Header("Play By Play")]
        public PlayByPlayController PlayByPlay;

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
            }
        }

        public void HealMonster (float amount)
        {
            ManagerBar.SetValue(ManagerBar.Percentage + amount);
            Monster.SetTrigger(AttackTrigger);
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
            }
        }

        public void SetBegActive (bool active)
        {

        }

        public void SetHealActive (bool active)
        {

        }
    }
}