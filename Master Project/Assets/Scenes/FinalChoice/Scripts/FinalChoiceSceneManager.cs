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

        public Sprite Nessie;
        public Sprite Cerberus;
        public Sprite Redacted;

        // Use this for initialization
        void Start() {
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
			//TODO: Honestly I have no idea what to do here.
		}
	}
}