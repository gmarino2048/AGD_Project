using Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FoodScoring
{
	public class InputHandler : MonoBehaviour
	{
		private readonly string _DIALOGUE_SCENE_NAME = "DialogueScene";
		private readonly string _MONOLOGUE_SCENE_NAME = "Monologue";

		private CombatInitiator _CombatInitiator;
		private GameNarrativeManager _GameNarrativeManager;
		private MonsterFactory _MonsterFactory;

		private bool _AreItemsLoaded = false;

		void Awake()
		{
			_CombatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
			_GameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
			_MonsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
			_AreItemsLoaded = true;
		}

		void Update ()
		{
            if (!_AreItemsLoaded || !Input.anyKey)
            {
				return;
			}

			if (_CombatInitiator == null || _GameNarrativeManager == null || _MonsterFactory == null)
			{
				SceneManager.LoadScene("Game OVer", LoadSceneMode.Single);
			}

			var monster = _MonsterFactory.LoadMonster(_GameNarrativeManager.CurrentStage.MonsterID);

			if (monster.AffectionValue <= monster.FightThreshold) // Not good enough! FIGHT! FIGHT!! FIGHT!!!
            {
                _CombatInitiator.InitiateCombat(_GameNarrativeManager.CurrentStage.MonsterID, 1 - monster.AffectionValue);
            }
            else
            {
				_GameNarrativeManager.DateableMonsterIDs.Add(_GameNarrativeManager.CurrentStage.MonsterID);
                if (!_GameNarrativeManager.AnyStagesLeft()) // No more monsters - onto the end
                {
					SceneManager.LoadScene(_MONOLOGUE_SCENE_NAME);
                }
                else // Onto the next monster
                {
					_GameNarrativeManager.StartNextStage();
                    SceneManager.LoadScene(_DIALOGUE_SCENE_NAME, LoadSceneMode.Single);
                }
            }
		}
	}
}