using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stirring
{
    public class ScoreKeeperBehavior : MonoBehaviour
    {
        [Header("Game Controls")]
        public TimerBehavior Timer;
        public SpoonBehavior Spoon;
        public float ScoreScaler = 300f;

        [Header("UI Elements")]
        public Text ScoreText;

        public float Score { get; private set; }

        public void ScoreGame ()
        {
            CalculateScore();
            SetScore();

            try
            {
                DishPreparationManager preparationManager = FindObjectOfType<DishPreparationManager>();
                GameNarrativeManager narrativeManager = FindObjectOfType<GameNarrativeManager>();
                DishScoreManager scoreManager = FindObjectOfType<DishScoreManager>();

                Guid monsterID = narrativeManager.CurrentStage.MonsterID;
                IngredientType currentIngredient = preparationManager.currentIngredient;
                scoreManager.AddIngredientToDish(monsterID, currentIngredient, Score);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                Debug.Log("Shaking Scene not Running in Game");
            }
        }

        void CalculateScore ()
        {
            Score = 1 / ((Spoon.Distance / ScoreScaler) + 1);
        }

        void SetScore ()
        {
            float scaledScore = (1 - Score) * 1000;
            int displayScore = Mathf.RoundToInt(scaledScore);

            string message = displayScore.ToString() + "/1000";
            ScoreText.text = message;
        }
    }
}