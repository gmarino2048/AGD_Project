using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class CerberusController : MonsterController
    {
        public string NeutralTrigger;

        bool Drooling;

        void Start()
        {
            Drooling = true;
            StartCoroutine(Drool(NeutralTrigger));
        }

        IEnumerator Drool(string triggerName)
        {
            while (Drooling)
            {
                float waitTime = Random.Range(1f, 8f);
                yield return new WaitForSeconds(waitTime);
                Animation.SetTrigger(triggerName);
            }
        }
    }
}