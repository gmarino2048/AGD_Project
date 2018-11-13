using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

namespace Ingredients {
    public class DragIngredient : MonoBehaviour
    {

        //distance of the mouse from the center of the spoon
        private Vector3 offset;

        //where mouse is
        private Vector3 mousePosition;

        //where mouse was last frame
        private Vector3 prevMousePosition;

        //how far the spoon traveled
        public float travelDistance = 0;

        /// <summary>
        /// The ingredient type for this ingredient
        /// </summary>
        public IngredientType ingredientType;

        public Vector3 originalPosition { get; private set; }

        /// <summary>
        /// The red X to show
        /// </summary>
		public GameObject incorrectIngredientMark;

        /// <summary>
        /// The ingredients manager
        /// </summary>
        public IngredientsManager ingredientsManager;

		private bool isLockedToLocation = false;

        // Use this for initialization
        void Start()
        {
            originalPosition = this.transform.position;
			incorrectIngredientMark.GetComponent<SpriteRenderer> ().enabled = false;
        }


        /// <summary>
        /// keeps track of distance traveled
        /// </summary>
        void Update()
        {
            if (isLockedToLocation || !ingredientsManager.IsIngredientTypeLegal(ingredientType))
            {
                return;
            }

            travelDistance += Vector3.Distance(mousePosition, prevMousePosition);
            prevMousePosition = mousePosition;
        }

        /// <summary>
        /// keeps the spoon for auto-centering on the mouse making it look disconnected from the previous frame
        /// </summary>
        void OnMousePressed()
        {
            if (isLockedToLocation || !ingredientsManager.IsIngredientTypeLegal(ingredientType))
            {
                return;
            }

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        /// <summary>
        /// moves the spoon
        /// </summary>
        void OnMouseDrag()
        {
			if (isLockedToLocation)
			{
                return;
            }
			if (!ingredientsManager.IsIngredientTypeLegal(ingredientType)) {
				var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
				var monsterFactory = GameObject.FindObjectOfType<MonsterFactory> ();
				var monsterData = monsterFactory.LoadMonster (gameNarrativeManager.CurrentStage.MonsterID);
				monsterData.UpdateAffectionFromIngredientSelection (ingredientType);
				//
				if (monsterData.AffectionValue <= monsterData.FightThreshold) {
					var combatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
					combatInitiator.InitiateCombat (gameNarrativeManager.CurrentStage.MonsterID, 1 - monsterData.AffectionValue);
				}
				StartCoroutine(ShowRedX());
				return;
			}
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            mousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = mousePosition;
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
            transform.position = choiceLocation.transform.position;
        }    

        public void SendBackToOriginalPosition()
        {
            transform.position = originalPosition;
        }

		IEnumerator ShowRedX()
		{
			incorrectIngredientMark.GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSeconds(2);
			incorrectIngredientMark.GetComponent<SpriteRenderer> ().enabled = false;
		}
    }
}