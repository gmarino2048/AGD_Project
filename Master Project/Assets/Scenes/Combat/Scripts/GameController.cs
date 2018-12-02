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

        public enum LastEvent
        {
            UserAttack,
            MonsterAttack,
            UserHeal,
            MonsterHeal,
            RoundComplete
        }

        public void HealPlayer (float Amount, LastEvent lastEvent)
        {

        }

        public void DamagePlayer (float Amount, LastEvent lastEvent)
        {
            if (Amount < 0)
            {
                HealPlayer(Amount * -1, lastEvent);
            }
        }

        public void HealMonster (float Amoun, LastEvent lastEvent)
        {

        }

        public void DamageMonster (float Amount, LastEvent lastEvent)
        {
            if (Amount < 0)
            {
                HealPlayer(Amount * -1, lastEvent);
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