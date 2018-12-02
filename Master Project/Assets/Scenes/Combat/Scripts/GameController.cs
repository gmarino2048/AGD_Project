using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class GameController : MonoBehaviour {

        [Header("Combat Values")]
        public InfoBarController HealthBar;
        public InfoBarController ManagerBar;

        public bool UserTurn { get; private set; }
        public bool MonsterTurn { get; private set; }
    }
}