using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grill
{
    public class ScoreKeeperBehavior : MonoBehaviour
    {

        [Header("Game Controls")]
        public TimerBehavior Timer;

        [Header("Score Settings")]
        public uint CookTarget;

        [Header("UI Stuff")]
        public CanvasManager Manager;
        public CanvasGroup ScoreDisplay;

        public uint Cooked { get; private set; }
        float AverageScore;

        public float Score { get; private set; }


        // Use this for initialization
        void Start()
        {
            Score = 1f;
            AverageScore = -1f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddScore (CookObjectController cookObject)
        {
            if (AverageScore < 0)
            {
                AverageScore = cookObject.Score;
                Cooked++;
            }
            else
            {
                float tempScore = AverageScore * Cooked;
                tempScore += cookObject.Score;
                AverageScore = tempScore / ++Cooked;
            }
        }
    }
}
