using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public abstract class MonsterController : MonoBehaviour
    {
        [Header("Animator")]
        public Animator Animation;

        [Header("Trigger Names")]
        public string GoodTrigger;
        public string BadTrigger;

        public void TriggerGood ()
        {
            Animation.SetTrigger(GoodTrigger);
        }

        public void TriggerBad ()
        {
            Animation.SetTrigger(BadTrigger);
        }
    }
}