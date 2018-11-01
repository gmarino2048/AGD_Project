using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Reason : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void reason()
        {
            bar.HandlePlayerCombatChoice(CombatChoice.Reason);
            ma.PlayerMoved = true;
        }
    }
}
