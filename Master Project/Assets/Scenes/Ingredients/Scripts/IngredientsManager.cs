using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
	public class IngredientsManager : MonoBehaviour
	{
    	private readonly Guid _NESSIE_GUID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");

		private DishPreparationManager _DishPreparationManager;

		private MonsterData _MonsterData;
		private List<IngredientType> _IngredientsAdded;

        public GameObject ingredientChoice;
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

            _MonsterData = monsterFactory.LoadMonster(_NESSIE_GUID);
            _IngredientsAdded = new List<IngredientType>();
            InstantiateIngredientChoice();
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
				_DishPreparationManager.StartPreparingDish(_NESSIE_GUID, _MonsterData.DesiredIngredients);
				_DishPreparationManager.GoToNextScene();
			}
		}

		public void InstantiateIngredientChoice() {
			int n = _MonsterData.DesiredIngredients.Count;
			if (n == 3) {
				GameObject choice1 = Instantiate(ingredientChoice, new Vector3((float)-2.5,(float)-.65,0), Quaternion.identity) as GameObject;
				GameObject choice2 = Instantiate(ingredientChoice, new Vector3(0,(float)-.65,0), Quaternion.identity) as GameObject;
				GameObject choice3 = Instantiate(ingredientChoice, new Vector3((float)2.5,(float)-.65,0), Quaternion.identity) as GameObject;
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice1.transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
				choice2.transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
				choice3.transform.localScale = new Vector3((float)1.5, (float)1.5, 0);
			}
			if (n == 4) {
				GameObject choice1 = Instantiate(ingredientChoice, new Vector3((float)-3,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice2 = Instantiate(ingredientChoice, new Vector3((float)-1,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice3 = Instantiate(ingredientChoice, new Vector3((float)1,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice4 = Instantiate(ingredientChoice, new Vector3((float)3,(float)-.65,(float)0), Quaternion.identity) as GameObject;
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice4.SetActive(true);
                choice1.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)0);
				choice2.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)0);
				choice3.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)0);
				choice4.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)0);
			}
			if (n == 5) {
				GameObject choice1 = Instantiate(ingredientChoice, new Vector3((float)-3,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice2 = Instantiate(ingredientChoice, new Vector3((float)-1.5,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice3 = Instantiate(ingredientChoice, new Vector3((float)0,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice4 = Instantiate(ingredientChoice, new Vector3((float)1.5,(float)-.65,(float)0), Quaternion.identity) as GameObject;
				GameObject choice5 = Instantiate(ingredientChoice, new Vector3((float)3,(float)-.65,(float)0), Quaternion.identity) as GameObject;
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice4.SetActive(true);
                choice5.SetActive(true);
                choice1.transform.localScale = new Vector3(1, 1, 0);
				choice2.transform.localScale = new Vector3(1, 1, 0);
				choice3.transform.localScale = new Vector3(1, 1, 0);
				choice4.transform.localScale = new Vector3(1, 1, 0);
				choice5.transform.localScale = new Vector3(1, 1, 0);
			}
		}
	}
}