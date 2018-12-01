using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monsters;


public class ScoreTextScript : MonoBehaviour {

    public Text textBox;


    // Use this for initialization
    void Start () {
        DishScoreManager _DishScoreManager;

        var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

        if (GameObject.Find("GameData") == null)
        {
            _DishScoreManager = gameObject.AddComponent(typeof(DishScoreManager)) as DishScoreManager;

            /*gameNarrativeManager = gameObject.AddComponent(typeof(GameNarrativeManager)) as GameNarrativeManager;
            gameNarrativeManager.Start();
            while (gameNarrativeManager.AnyStagesLeft())
            {
                gameNarrativeManager.StartNextStage();
                gameNarrativeManager.DateableMonsterIDs.Add(gameNarrativeManager.CurrentStage.MonsterID);
            }*/

        }
        else
        {
            _DishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
            //gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
        }

        DishScoreManager scoreManager = FindObjectOfType<DishScoreManager>();

        float score = scoreManager.ScoreDish(gameNarrativeManager.CurrentStage.MonsterID);

        textBox.text = "Score: "+ (1-score)*1000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
