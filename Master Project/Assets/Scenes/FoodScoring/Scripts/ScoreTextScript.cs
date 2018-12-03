using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monsters;

namespace FoodScoring {
    public class ScoreTextScript : MonoBehaviour {
        public Text textBox;

        public float Score { get; private set; }
        public float Threshold { get; private set; }

        public AudioSource Source;
        public AudioClip OrderUp;
        public AudioClip Approve;
        public AudioClip Disapprove;
        public float Volume = 0.75f;

        // Use this for initialization
        void Start () {
            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            var dishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();

            float score = dishScoreManager == null ? 0.1f : dishScoreManager.ScoreDish(gameNarrativeManager.CurrentStage.MonsterID);

            try
            {
                var monster = monsterFactory.LoadMonster(gameNarrativeManager.CurrentStage.MonsterID);
                Score = monster.AffectionValue;
                Threshold = monster.FightThreshold;
            }
            catch (Exception)
            {
                Score = 0.9f;
                Threshold = 0.5f;
            }

            textBox.text = "Score: " + Mathf.RoundToInt((1 - score) * 1000);

            StartCoroutine(PlaySound());
        }

        public IEnumerator PlaySound ()
        {
            yield return new WaitForSeconds(1f);
            Source.PlayOneShot(OrderUp, Volume);
            yield return new WaitWhile(() => Source.isPlaying);
            yield return new WaitForSeconds(0.5f);

            if (Score > Threshold) Source.PlayOneShot(Approve, Volume);
            else Source.PlayOneShot(Disapprove, Volume);
        }
    }
}