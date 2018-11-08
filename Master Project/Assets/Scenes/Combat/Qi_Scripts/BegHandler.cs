using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class BegHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        public GameObject BegHider;
        public bool begshown;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
            BegHider.SetActive(true);
            begshown = false;
        }

        public void Beg()
        {
            if (!begshown)
            {
                ma.PlayerMoved = true;
            }
            bar.HandlePlayerCombatChoice(CombatChoice.Beg);
            begshown = true;
            BegHider.SetActive(true);
        }
    }
}
