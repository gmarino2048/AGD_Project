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

        public uint Cooked { get; private set; }
        float AverageScore;

        public float Score { get; private set; }

        bool ScoreDone = false;

        // Use this for initialization
        void Start()
        {
            Score = 1f;
            AverageScore = -1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Timer.GameOver && !ScoreDone)
            {
                Score = CalculateScore();
                ScoreDone = true;
            }
        }

        public void EndGame()
        {
            Score = CalculateScore();
            StartCoroutine(Manager.ShowScore(Score));
        }

        float CalculateScore ()
        {
            float scaler = Cooked / CookTarget;

            return scaler > 1f ? AverageScore : AverageScore * scaler;
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
