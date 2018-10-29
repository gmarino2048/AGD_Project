using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class FlatterHandler : MonoBehaviour
    {
        private MonsterAction ma;
        public ManagerBar bar;
        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void Flatter()
        {
            bar.HandlePlayerCombatChoice(CombatChoice.Flatter);
            ma.PlayerMoved = true;
        }
    }
}
