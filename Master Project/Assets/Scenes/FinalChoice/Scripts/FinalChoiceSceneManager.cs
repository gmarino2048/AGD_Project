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
        void Start() {
            resultScreen.enabled = false;

            _MonsterFactory = GameObject.FindObjectOfType<MonsterFactory>();

            CreateChoiceButtons();

        }

		private void CreateChoiceButtons()
		{
			var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

			foreach (var monsterID in gameNarrativeManager.DateableMonsterIDs)
			{
				var monsterData = _MonsterFactory.LoadMonster(monsterID);
				var button = (Button) Instantiate(choiceButtonPrefab, choicesParent);

				button.GetComponentInChildren<Text>().text = monsterData.Name;
				button.onClick.AddListener(() => ChooseMonster(monsterID));

                if(monsterData.ToString()== "Nessie"){
                    button.GetComponent<Image>().sprite = Nessie;
                }
                if (monsterData.ToString() == "Cerberus")
                {
                    button.GetComponent<Image>().sprite = Cerberus;
                }
                if (monsterData.ToString() == "Redacted")
                {
                    button.GetComponent<Image>().sprite = Redacted;
                }
            }
		}


		private void ChooseMonster(Guid monsterID)
		{
			var monsterData = _MonsterFactory.LoadMonster(monsterID);
			Debug.Log(monsterData.Name + " was chosen");

            if (monsterData.ToString() == "Nessie")
            {
                resultScreen.sprite = NessieSplash;
            }
            if (monsterData.ToString() == "Cerberus")
            {
                resultScreen.sprite = CerberusSplash;
            }
            if (monsterData.ToString() == "Redacted")
            {
                resultScreen.sprite = RedactedSplash;
            }


            resultScreen.enabled = false;
        }
	}
}