using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
	public class IngredientsManager : MonoBehaviour
	{
    	private readonly Guid _NESSIE_ID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");

		private DishPreparationManager _DishPreparationManager;

		private MonsterData _MonsterData;
		private List<IngredientType> _IngredientsAdded;

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

			_MonsterData = monsterFactory.LoadMonster(_NESSIE_ID);
			_IngredientsAdded = new List<IngredientType>();
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
				_DishPreparationManager.StartPreparingDish(_MonsterData.DesiredIngredients);
				_DishPreparationManager.GoToNextScene();
			}
		}
	}
}