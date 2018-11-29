using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
	public class IngredientsManager : MonoBehaviour
	{
		private CombatInitiator _CombatInitiator;
		private DishPreparationManager _DishPreparationManager;
		private GameNarrativeManager _GameNarrativeManager;
		private List<IngredientType> _IngredientsAdded;
		private MonsterData _MonsterData;

        public GameObject ingredientChoiceLocationPrefab;

		void Start()
        {
            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
			if (monsterFactory == null)
			{
				throw new Exception("MonsterFactory did not exist in scene");
			}

			_DishPreparationManager = GameObject.FindObjectOfType<DishPreparationManager>();
			if (_DishPreparationManager == null)
			{
				throw new Exception("DishPreparationManager did not exist in scene");
			}

        	_GameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
			if (_GameNarrativeManager == null)
			{
				throw new Exception("GameNarrativeManager did not exist in scene");
			}

            _MonsterData = monsterFactory.LoadMonster(_GameNarrativeManager.CurrentStage.MonsterID);
            _IngredientsAdded = new List<IngredientType>();

            InstantiateIngredientChoiceLocations();
        }

		public bool IsIngredientTypeLegal(IngredientType ingredientType)
		{
			return _MonsterData.DesiredIngredients.Contains(ingredientType) && !_IngredientsAdded.Contains(ingredientType);
		}

		public void AddIngredient(IngredientType ingredientType)
		{
			_IngredientsAdded.Add(ingredientType);

			if (_IngredientsAdded.Count == _MonsterData.DesiredIngredients.Count)
			{
				_DishPreparationManager.StartPreparingDish(
					_GameNarrativeManager.CurrentStage.MonsterID,
					_MonsterData.DesiredIngredients
				);
				_DishPreparationManager.GoToNextScene();
			}
		}

		public void InstantiateIngredientChoiceLocations() {
			int n = _MonsterData.DesiredIngredients.Count;
			if (n == 3) {
				GameObject choice1 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)-2.5,(float)-1.08,0), Quaternion.identity) as GameObject;
				GameObject choice2 = Instantiate(ingredientChoiceLocationPrefab, new Vector3(0,(float)-1.08,0), Quaternion.identity) as GameObject;
				GameObject choice3 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)2.5,(float)-1.08,0), Quaternion.identity) as GameObject;
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice1.transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
				choice2.transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
				choice3.transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
			}
			if (n == 4) {
				GameObject choice1 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)-3,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice2 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)-1,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice3 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)1,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice4 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)3,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice4.SetActive(true);
                choice1.transform.localScale = new Vector3((float)1.2, (float)1.5, (float)0);
				choice2.transform.localScale = new Vector3((float)1.2, (float)1.5, (float)0);
				choice3.transform.localScale = new Vector3((float)1.2, (float)1.5, (float)0);
				choice4.transform.localScale = new Vector3((float)1.2, (float)1.5, (float)0);
			}
			if (n == 5) {
				GameObject choice1 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)-3,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice2 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)-1.5,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice3 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)0,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice4 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)1.5,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
				GameObject choice5 = Instantiate(ingredientChoiceLocationPrefab, new Vector3((float)3,(float)-1.08,(float)0), Quaternion.identity) as GameObject;
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice4.SetActive(true);
                choice5.SetActive(true);
				choice1.transform.localScale = new Vector3((float).9, (float)1.5, 0);
				choice2.transform.localScale = new Vector3((float).9, (float)1.5, 0);
				choice3.transform.localScale = new Vector3((float).9, (float)1.5, 0);
				choice4.transform.localScale = new Vector3((float).9, (float)1.5, 0);
				choice5.transform.localScale = new Vector3((float).9, (float)1.5, 0);
			}
		}
	}
}