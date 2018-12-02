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

        public CanvasGroup Group;
        public CanvasGroup PurpleStuff;


        // Use this for initialization
        void Awake() {

            Group.gameObject.SetActive(false);
            PurpleStuff.gameObject.SetActive(false);

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

                button.GetComponentInChildren<Text>().text = "";
                button.onClick.AddListener(() => StartCoroutine(ChooseMonster(monsterID)));

                if(monsterData.Name == "Nessie"){
                    button.GetComponent<Image>().sprite = Nessie;
                    button.transform.position = new Vector3(-300,250,0);
                }
                if (monsterData.Name == "Cerberus")
                {
                    button.GetComponent<Image>().sprite = Cerberus;
                    button.transform.position = new Vector3(250, 0, 0);
                }
                if (monsterData.Name == "[REDACTED]")
                {
                    button.GetComponent<Image>().sprite = Redacted;
                    button.transform.position = new Vector3(-300, -250, 0);
                }
            }
            //resultScreen.enabled = true;
        }


		public IEnumerator ChooseMonster(Guid monsterID)
		{
            Group.gameObject.SetActive(true);
            PurpleStuff.gameObject.SetActive(true);
            var monsterData = _MonsterFactory.LoadMonster(monsterID);
			Debug.Log(monsterData.Name + " was chosen");

            if (monsterData.Name == "Nessie")
            {
                resultScreen.sprite = NessieSplash;
                yield return FadeCanvas(PurpleStuff, 0, 1, 1);
                yield return FadeCanvas(Group, 0, 1, 1);

            }
            if (monsterData.Name == "Cerberus")
            {
                resultScreen.sprite = CerberusSplash;
                yield return FadeCanvas(PurpleStuff, 0, 1, 1);
                yield return FadeCanvas(Group, 0, 1, 1);

            }
            if (monsterData.Name == "[REDACTED]")
            {
                resultScreen.sprite = RedactedSplash;
                //background.GetComponent<Image>().enabled = false;
                yield return FadeCanvas(PurpleStuff, 0, 1, 1);
                yield return FadeCanvas(Group, 0, 1, 1);
            }



        }

        public IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float duration, float endAlpha)
        {
            float startTime = Time.time;

            float change = (endAlpha - startAlpha) / duration;

            while (Time.time - startTime <= duration)
            {
                float currentTime = Time.time - startTime;
                canvas.alpha = startAlpha + (change * currentTime);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}