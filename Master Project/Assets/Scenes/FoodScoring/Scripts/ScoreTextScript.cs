using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Monsters;


public class ScoreTextScript : MonoBehaviour {

    public Text textBox;


    // Use this for initialization
    void Start () {
        var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

        DishScoreManager scoreManager = FindObjectOfType<DishScoreManager>();

        float score = scoreManager.ScoreDish(gameNarrativeManager.CurrentStage.MonsterID);

        textBox.text = "Score: "+score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
