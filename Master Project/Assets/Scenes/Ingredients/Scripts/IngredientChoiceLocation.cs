using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

namespace Ingredients
{
    public class IngredientChoiceLocation : MonoBehaviour
    {
        private IngredientsManager ingredientsManager;

        private IngredientType? chosenIngredientType;

        BoxCollider2D boxCollider;


        // Use this for initialization
        void Start ()
        {
            ingredientsManager = GameObject.FindObjectOfType<IngredientsManager>();
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
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

            DragIngredient ingredient = null;
            foreach (var overlapObject in overlap)
            {
                var dragIngredientComponent = overlapObject.gameObject.GetComponent<DragIngredient>();
                if (dragIngredientComponent != null)
                {
                    ingredient = dragIngredientComponent;
                }
            }
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
    }
}