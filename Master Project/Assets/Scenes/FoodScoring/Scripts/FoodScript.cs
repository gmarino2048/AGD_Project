using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using UnityEngine.UI;

namespace FoodScoring {
    public class FoodScript : MonoBehaviour
    {
        public GameObject FoodSprite;

        public Sprite Nessie;
        public Sprite Cerberus;
        public Sprite Redacted;

        // Use this for initialization
        void Awake () {
            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

            var monsterData = monsterFactory == null ? null : monsterFactory.LoadMonster(gameNarrativeManager.CurrentStage.MonsterID);

            if (monsterData == null || monsterData.Name == "Nessie")
            {
                FoodSprite.GetComponent<Image>().sprite = Nessie;
                FoodSprite.transform.localScale = new Vector3(.7f, .7f, 0);
            }
            else if (monsterData.Name == "Cerberus")
            {
                FoodSprite.GetComponent<Image>().sprite = Cerberus;
                FoodSprite.transform.localScale = new Vector3(.6f, .6f, 0);
            }
            else if (monsterData.Name == "[REDACTED]")
            {
                FoodSprite.GetComponent<Image>().sprite = Redacted;
                //RectTransform rt = (RectTransform)FoodSprite.transform;
                //FoodSprite.GetComponent(rt).sizeDelta = new Vector2(1920, 1080);
                FoodSprite.transform.localScale = new Vector3(.75f, .75f, 0);
            }
        }
    }
}