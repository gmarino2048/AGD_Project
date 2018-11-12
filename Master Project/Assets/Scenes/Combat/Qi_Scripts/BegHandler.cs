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
        public bool isActive;

        private void Awake()
        {
            ma = GameObject.Find("Nessie").GetComponent<MonsterAction>();
            BegHider = GameObject.Find("BegHider");
            BegHider.SetActive(true);
            begshown = false;
            isActive = true;
        }

        public void Beg()
        {
            if (!begshown && !isActive)
            {
                ma.PlayerMoved = true;
                bar.HandlePlayerCombatChoice(CombatChoice.Beg);
            }
            begshown = true;
            BegHider.SetActive(true);
            isActive = true;
        }
    }
}
