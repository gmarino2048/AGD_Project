using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grill
{
    public class ScoreKeeperBehavior : MonoBehaviour
    {

        [Header("Game Controls")]
        public TimerBehavior Timer;

        [Header("Score Settings")]
        public uint CookTarget;
        public Text PattiesCooked;
        public string Message = "Patties Cooked: ";

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
            Cooked = 0;

            PattiesCooked.text = Message + Cooked.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (Timer.GameOver && !ScoreDone)
            {
                Score = CalculateScore();
                ScoreDone = true;
            }

            PattiesCooked.text = Message + Cooked.ToString();
        }

        public void EndGame()
        {
            Score = CalculateScore();

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
