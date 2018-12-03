using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatReactionController : MonoBehaviour
    {

        [Header("Animations")]
        public Animator CombatOverlay;
        public string Heal = "heal";
        public string Damage = "damage";

        public void PlayHeal()
        {
            CombatOverlay.SetTrigger(Heal);
        }

        public void PlayDamage()
        {
            CombatOverlay.SetTrigger(Damage);
        }
    }
}