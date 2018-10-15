using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class FreeFoodHandler : MonoBehaviour
    {

        private MonsterAction ma;

        public ManagerBar bar;

        private void Awake()
        {
            ma = GameObject.Find("Rogue_06").GetComponent<MonsterAction>();
        }
        public void FreeFood()
        {
            Debug.Log("FreeFood");
            bar.IncrementValue(-10); //TODO: Rewrite for handling varying degrees of success
            ma.PlayerMoved = true;
        }
    }
}
