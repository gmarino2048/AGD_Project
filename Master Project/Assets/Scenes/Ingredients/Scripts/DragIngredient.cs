using Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ingredients
{
    public class DragIngredient : MonoBehaviour
    {
        /// <summary>
        /// The ingredient type for this ingredient
        /// </summary>
        public IngredientType ingredientType;
        
        private BoxCollider2D _Collider;
        private GameNarrativeManager _GameNarrativeManager;
        private Image _IncorrectIngredientMarker;
        private IngredientsManager _IngredientsManager;
        private MonsterFactory _MonsterFactory;
        private Vector3 _Offset;

		private bool isLockedToLocation = false;

        // Use this for initialization
        void Awake()
        {
            _Collider = this.gameObject.GetComponent<BoxCollider2D>();
            _GameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
            _IncorrectIngredientMarker = GameObject.FindGameObjectWithTag("RedX").GetComponent<Image>();
            _IngredientsManager = GameObject.FindObjectOfType<IngredientsManager>();
            _MonsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
        }

        void OnMouseDown()
        {
            if (isLockedToLocation || _IncorrectIngredientMarker.enabled)
            {
                return;
            }
            
			if (!_IngredientsManager.IsIngredientTypeLegal(ingredientType)) {
                var monsterData = _MonsterFactory.LoadMonster(_GameNarrativeManager.CurrentStage.MonsterID);
                monsterData.UpdateAffectionFromIngredientSelection(ingredientType);

                if (monsterData.AffectionValue <= monsterData.FightThreshold) {
                    var combatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
                    combatInitiator.InitiateCombat(_GameNarrativeManager.CurrentStage.MonsterID, 1 - monsterData.AffectionValue);
                    return;
                }

                StartCoroutine(ShowRedX());
                return;
			}

            _Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        void OnMouseDrag()
        {
            if (isLockedToLocation || !_IngredientsManager.IsIngredientTypeLegal(ingredientType) || _IncorrectIngredientMarker.enabled) {
                return;
            }

            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            transform.position = Camera.main.ScreenToWorldPoint(cursorPoint) + _Offset;
        }

        /// <summary>
        /// Locks an ingredient to a specific ingredient choice location
        /// </summary>
        /// <param name="choiceLocation">The location to lock an ingredient to</param>
        public void LockToChoiceLocation(IngredientChoiceLocation choiceLocation)
        {
            if (isLockedToLocation)
            {
                return;
            }

            isLockedToLocation = true;
            transform.position = choiceLocation.transform.position + (Vector3.up * (_Collider.bounds.size.y/2));
        }

		IEnumerator ShowRedX()
		{
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            _IncorrectIngredientMarker.transform.position = screenPosition;
			_IncorrectIngredientMarker.enabled = true;
			yield return new WaitForSeconds(2);
			_IncorrectIngredientMarker.enabled = false;
		}
    }
}