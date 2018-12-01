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
    void Awake () {

        GameNarrativeManager gameNarrativeManager;

        if (GameObject.Find("GameData") == null)
        {
            _MonsterFactory = gameObject.AddComponent(typeof(MonsterFactory)) as MonsterFactory;

            gameNarrativeManager = gameObject.AddComponent(typeof(GameNarrativeManager)) as GameNarrativeManager;
            gameNarrativeManager.Start();
            while (gameNarrativeManager.AnyStagesLeft())
            {
                gameNarrativeManager.StartNextStage();
                gameNarrativeManager.DateableMonsterIDs.Add(gameNarrativeManager.CurrentStage.MonsterID);
            }

        }
        else
        {
            _MonsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
        }


        var monsterData = _MonsterFactory.LoadMonster(gameNarrativeManager.CurrentStage.MonsterID);
        Debug.Log(monsterData.Name);

        if (monsterData.Name == "Nessie")
        {
            FoodSprite.GetComponent<Image>().sprite = Nessie;
            FoodSprite.transform.localScale = new Vector3(.7f, .7f, 0);
        }
        if (monsterData.Name == "Cerberus")
        {
            FoodSprite.GetComponent<Image>().sprite = Cerberus;
            FoodSprite.transform.localScale = new Vector3(.6f, .6f, 0);
        }
        if (monsterData.Name == "[REDACTED]")
        {
            FoodSprite.GetComponent<Image>().sprite = Redacted;
            //RectTransform rt = (RectTransform)FoodSprite.transform;
            //FoodSprite.GetComponent(rt).sizeDelta = new Vector2(1920, 1080);
            FoodSprite.transform.localScale = new Vector3(.75f, .75f, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
