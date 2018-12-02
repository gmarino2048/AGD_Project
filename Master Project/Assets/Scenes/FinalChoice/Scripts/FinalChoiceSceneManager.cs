using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FinalChoice
{
	public class FinalChoiceSceneManager : MonoBehaviour
	{
		/// <summary>
		/// The prefab to use to create buttons for each choice
		/// </summary>
		public Button choiceButtonPrefab;

		/// <summary>
		/// What to parent the choice buttons to
		/// </summary>
		public Transform choicesParent;

		private MonsterFactory _MonsterFactory;

        public Image resultScreen;

        public Sprite Nessie;
        public Sprite Cerberus;
        public Sprite Redacted;

        public Sprite NessieSplash;
        public Sprite CerberusSplash;
        public Sprite RedactedSplash;

        // Use this for initialization
        void Awake() {

            resultScreen.enabled = false;


            if (GameObject.Find("GameData") == null)
            {
                _MonsterFactory = gameObject.AddComponent(typeof(MonsterFactory)) as MonsterFactory;

                var gameNarrativeManager = gameObject.AddComponent(typeof(GameNarrativeManager)) as GameNarrativeManager;
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
            }

            CreateChoiceButtons();

        }

		private void CreateChoiceButtons()
		{
			var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

            foreach (var monsterID in gameNarrativeManager.DateableMonsterIDs)
			{
				var monsterData = _MonsterFactory.LoadMonster(monsterID);
				var button = (Button) Instantiate(choiceButtonPrefab, choicesParent);

                Debug.Log(monsterData.Name);



                button.GetComponentInChildren<Text>().text = "";
				button.onClick.AddListener(() => ChooseMonster(monsterID));

                if(monsterData.Name == "Nessie"){
                    button.GetComponent<Image>().sprite = Nessie;
                }
                if (monsterData.Name == "Cerberus")
                {
                    button.GetComponent<Image>().sprite = Cerberus;
                }
                if (monsterData.Name == "[REDACTED]")
                {
                    button.GetComponent<Image>().sprite = Redacted;
                }
            }
            //resultScreen.enabled = true;
        }


		public void ChooseMonster(Guid monsterID)
		{
			var monsterData = _MonsterFactory.LoadMonster(monsterID);
			Debug.Log(monsterData.Name + " was chosen");

            if (monsterData.Name == "Nessie")
            {
                resultScreen.sprite = NessieSplash;
                //background.GetComponent<Image>().enabled = false;
                resultScreen.enabled = true;
            }
            if (monsterData.Name == "Cerberus")
            {
                resultScreen.sprite = CerberusSplash;
               //background.GetComponent<Image>().enabled = false;
                resultScreen.enabled = true;
            }
            if (monsterData.Name == "[REDACTED]")
            {
                resultScreen.sprite = RedactedSplash;
                //background.GetComponent<Image>().enabled = false;
                resultScreen.enabled = true;
            }



        }
	}
}