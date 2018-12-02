using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monsters;

namespace FoodScoring {
    public class ScoreTextScript : MonoBehaviour {
        public Text textBox;

        public AudioSource PositiveResult;
        public AudioSource NegativeResult;

        // Use this for initialization
        IEnumerator Start () {
            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            var dishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();

            float score = dishScoreManager == null ? 0.1f : dishScoreManager.ScoreDish(gameNarrativeManager.CurrentStage.MonsterID);

            textBox.text = "Score: "+ (1-score)*1000;
            Debug.Log("how");
            yield return new WaitForSeconds(1);
            Debug.Log("....");
            if (monsterFactory != null){
                var monster = monsterFactory.LoadMonster(gameNarrativeManager.CurrentStage.MonsterID);
                if (monster.AffectionValue >= monster.FightThreshold)
                {
                    PositiveResult.Play();
                }
                else {
                    NegativeResult.Play();
                }
            }
            else{
                NegativeResult.Play();
            }
        }

       /* IEnumerator delay(int amount){
            Debug.Log("what");
            yield return new WaitForSeconds(amount);
        }*/
    }
}