using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monsters;

namespace FoodScoring {
    public class ScoreTextScript : MonoBehaviour {
        public Text textBox;

        public AudioSource Source;
        public AudioClip Sound;
        float Volume = 0.75f;

        // Use this for initialization
        void Start () {
            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            var dishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();

            float score = dishScoreManager == null ? 0.1f : dishScoreManager.ScoreDish(gameNarrativeManager.CurrentStage.MonsterID);

            textBox.text = "Score: " + Mathf.RoundToInt((1 - score) * 1000);

            StartCoroutine(PlaySound());
        }

        public IEnumerator PlaySound ()
        {
            yield return new WaitForSeconds(1f);
            Source.PlayOneShot(Sound, Volume);
        }
    }
}