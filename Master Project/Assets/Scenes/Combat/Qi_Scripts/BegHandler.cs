using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class BegHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        public GameObject beg;
        public bool begshown;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
            beg.SetActive(false);
            begshown = false;
        }

        public void Beg()
        {
            bar.IncrementValue(-35); //TODO: Rewrite for handling varying degrees of success
            begshown = true;
            beg.SetActive(false);
            ma.PlayerMoved = true;
        }
    }
}
