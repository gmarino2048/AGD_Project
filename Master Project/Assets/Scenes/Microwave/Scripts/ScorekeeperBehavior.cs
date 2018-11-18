using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microwave
{
    public class ScorekeeperBehavior : MonoBehaviour
    {
        [Header("Game Controller")]
        public TimerBehavior Timer;

        [Header("UI Elements")]
        public Text ScoreText;

        public float Score { get; private set; }

        private void Start()
        {
            Score = 1f;
        }

        public void CalculateScore ()
        {
            float sqrtScore = Mathf.Sqrt(Timer.TimeRemaining < 0 ? 1 : Timer.TimeRemaining / Timer.GameTime);
            Score = sqrtScore;
        }

        public void SetScore ()
        {
            CalculateScore();
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
                Debug.Log("Microwave Scene not Running in Game");
            }

            int displayScore = Mathf.RoundToInt((1f - Score) * 1000f);
            string displayMessage = displayScore.ToString() + "/1000";
            ScoreText.text = displayMessage;
        }
    }
}