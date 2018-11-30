using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using UnityEngine.UI;

public class FoodScript : MonoBehaviour {

    private MonsterFactory _MonsterFactory;

    public GameObject FoodSprite;

    public Sprite Nessie;
    public Sprite Cerberus;
    public Sprite Redacted;

    // Use this for initialization
    void Start () {

        var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
        var monsterData = _MonsterFactory.LoadMonster(gameNarrativeManager.CurrentStage.MonsterID);

        //if (monsterData.ToString() == "Nessie")
        {
            Debug.Log("grrrr");
            FoodSprite.GetComponent<Image>().sprite = Nessie;
        }
        if (monsterData.ToString() == "Cerberus")
        {
            FoodSprite.GetComponent<Image>().sprite = Cerberus;
        }
        if (monsterData.ToString() == "Redacted")
        {
            FoodSprite.GetComponent<Image>().sprite = Redacted;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
