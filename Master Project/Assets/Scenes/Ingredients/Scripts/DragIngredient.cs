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
        
        public BoxCollider2D Collider;
        public GameNarrativeManager NarrativeManager;
        public Image XMarker;
        public IngredientsManager IngredientManager;
        public MonsterFactory MonsterFactory;
        public Vector3 Offset;

		private bool isLockedToLocation = false;

        // Use this for initialization
        void Awake()
        {
            Collider = gameObject.GetComponent<BoxCollider2D>();
            NarrativeManager = FindObjectOfType<GameNarrativeManager>();
            XMarker = GameObject.FindGameObjectWithTag("RedX").GetComponent<Image>();
            IngredientManager = FindObjectOfType<IngredientsManager>();
            MonsterFactory = FindObjectOfType<MonsterFactory>();
        }

        void OnMouseDown()
        {
            if (isLockedToLocation || XMarker.enabled)
            {
                return;
            }
            
			if (!IngredientManager.IsIngredientTypeLegal(ingredientType)) {
                var monsterData = MonsterFactory.LoadMonster(NarrativeManager.CurrentStage.MonsterID);
                monsterData.UpdateAffectionFromIngredientSelection(ingredientType);

                if (monsterData.AffectionValue <= monsterData.FightThreshold) {
                    var combatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
                    combatInitiator.InitiateCombat(NarrativeManager.CurrentStage.MonsterID, 1 - monsterData.AffectionValue);
                    return;
                }

                StartCoroutine(ShowRedX());
                return;
			}

            Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        void OnMouseDrag()
        {
            if (isLockedToLocation || !IngredientManager.IsIngredientTypeLegal(ingredientType) || XMarker.enabled) {
                return;
            }

            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            transform.position = Camera.main.ScreenToWorldPoint(cursorPoint) + Offset;
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
            transform.position = choiceLocation.transform.position + (Vector3.up * (Collider.bounds.size.y/2));
        }

		IEnumerator ShowRedX()
		{
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            XMarker.transform.position = screenPosition;
			XMarker.enabled = true;
			yield return new WaitForSeconds(2);
			XMarker.enabled = false;
		}
    }
}