using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

namespace Ingredients
{
    public class IngredientChoiceLocation : MonoBehaviour
    {
        public IngredientsManager ingredientsManager;

        private IngredientType? chosenIngredientType;

		public GameObject incorrectIngredientMark;

        BoxCollider2D boxCollider;

        // Use this for initialization
        void Start ()
        {
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
			incorrectIngredientMark = GameObject.Find ("RedX");
			incorrectIngredientMark.GetComponent<SpriteRenderer> ().enabled = false;
        }

	
        
        // Update is called once per frame
		void Update ()
        {
            if (chosenIngredientType.HasValue)
                return;

            Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
            if (overlap.Length < 1)
            {
                return;
            }

            var overlapObject = overlap[0].gameObject;
            var ingredient = overlapObject.GetComponent<DragIngredient>();
            if (ingredient == null)
            {
                return;
            }

            if (ingredientsManager.IsIngredientTypeLegal(ingredient.ingredientType))
            {
                ingredient.LockToChoiceLocation(this);
                ingredientsManager.AddIngredient(ingredient.ingredientType);
                chosenIngredientType = ingredient.ingredientType;
            }
            else
            {
                //ingredient.SendBackToOriginalPosition();
                //TODO: Show an 'x' saying no!
				//StartCoroutine(ShowRedX());

            }
        }
			

		IEnumerator ShowRedX()
		{
			incorrectIngredientMark.GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSeconds(2);
			incorrectIngredientMark.GetComponent<SpriteRenderer> ().enabled = false;
		}
    }
}