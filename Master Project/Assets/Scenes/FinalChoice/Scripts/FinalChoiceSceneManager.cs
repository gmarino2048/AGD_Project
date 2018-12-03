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

        public Button Nessie;
        public Button Cerberus;
        public Button Redacted;

        public Sprite NessieSplash;
        public Sprite CerberusSplash;
        public Sprite RedactedSplash;

        public CanvasGroup Group;
        public CanvasGroup PurpleStuff;

        public AudioSource Phone;

        public AudioSource InitialMusic;

        public AudioSource FinalMusic;

        // Use this for initialization
        void Awake() {

            Group.gameObject.SetActive(false);
            PurpleStuff.gameObject.SetActive(false);

            Nessie.gameObject.SetActive(false);
            Cerberus.gameObject.SetActive(false);
            Redacted.gameObject.SetActive(false);

            if (GameObject.Find("GameData") == null)
            {
                _MonsterFactory = gameObject.AddComponent(typeof(MonsterFactory)) as MonsterFactory;

                GameNarrativeManager gameNarrativeManager = gameObject.AddComponent<GameNarrativeManager>();
                gameNarrativeManager.Start();
                while (gameNarrativeManager.AnyStagesLeft())
                {
                    gameNarrativeManager.StartNextStage();
                    gameNarrativeManager.DateableMonsterIDs.Add(gameNarrativeManager.CurrentStage.MonsterID);
                }
                gameNarrativeManager.DateableMonsterIDs.Remove(gameNarrativeManager.DateableMonsterIDs[0]);
            }
            else
            {
                _MonsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
                var GameSettings = GameObject.FindObjectOfType<GameSettings>();
                InitialMusic.volume = GameSettings.MusicVolume * GameSettings.MasterVolume;
                FinalMusic.volume = GameSettings.MusicVolume * GameSettings.MasterVolume;

                Phone.volume = GameSettings.SfxVolume * GameSettings.MasterVolume;
            }

            CreateChoiceButtons();

        }

		private void CreateChoiceButtons()
		{
			var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();

            foreach (var monsterID in gameNarrativeManager.DateableMonsterIDs)
			{
				var monsterData = _MonsterFactory.LoadMonster(monsterID);

                if(monsterData.Name == "Nessie"){
                    Nessie.gameObject.SetActive(true);
                    Nessie.onClick.AddListener(() => StartCoroutine(ChooseMonster(monsterID)));
                }
                if (monsterData.Name == "Cerberus")
                {
                    Cerberus.gameObject.SetActive(true);
                    Cerberus.onClick.AddListener(() => StartCoroutine(ChooseMonster(monsterID)));
                }
                if (monsterData.Name == "[REDACTED]")
                {
                    Redacted.gameObject.SetActive(true);
                    Redacted.onClick.AddListener(() => StartCoroutine(ChooseMonster(monsterID)));
                }
            }
            //resultScreen.enabled = true;
        }


		public IEnumerator ChooseMonster(Guid monsterID)
		{
            Group.gameObject.SetActive(true);
            PurpleStuff.gameObject.SetActive(true);
            var monsterData = _MonsterFactory.LoadMonster(monsterID);
            InitialMusic.Stop();
            //Debug.Log(monsterData.Name + " was chosen");

            Phone.Play();

            if (monsterData.Name == "Nessie")
            {
                resultScreen.sprite = NessieSplash;
                yield return FadeCanvas(PurpleStuff, 0, 3.5f, 1);
                //FinalMusic.clip = FinalChoiceMusic;
                FinalMusic.Play();
                yield return FadeCanvas(Group, 0, 1, 1);

            }
            if (monsterData.Name == "Cerberus")
            {
                resultScreen.sprite = CerberusSplash;
                yield return FadeCanvas(PurpleStuff, 0, 3.5f, 1);
                FinalMusic.Play();
                yield return FadeCanvas(Group, 0, 1, 1);

            }
            if (monsterData.Name == "[REDACTED]")
            {
                resultScreen.sprite = RedactedSplash;
                //background.GetComponent<Image>().enabled = false;
                yield return FadeCanvas(PurpleStuff, 0, 3.5f, 1);
                FinalMusic.Play();
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