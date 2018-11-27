using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stirring
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Ice Cream Assets")]
        public string IceCreamTrigger = "Ice Cream";
        public Sprite IceCreamOverlay;

        [Header("Eggs Assets")]
        public string EggsTrigger = "Eggs";
        public Sprite EggsOverlay;

        [Header("Chili Assets")]
        public string ChiliTrigger = "Chili";
        public Sprite ChiliOverlay;

        [Header("Scene Objects")]
        public BowlBehavior Bowl;
        public SpoonBehavior OverlayHelper;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.IceCream:
                        SetParameters(IceCreamTrigger, IceCreamOverlay);
                        break;

                    case IngredientType.Eggs:
                        SetParameters(EggsTrigger, EggsOverlay);
                        break;

                    case IngredientType.VoidGoo:
                        SetParameters(ChiliTrigger, ChiliOverlay);
                        break;

                    default:
                        Debug.LogError("Incorrect Ingredient -- defaulting to ice cream");
                        SetParameters(IceCreamTrigger, IceCreamOverlay);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- defaulting to ice cream");
                SetParameters(IceCreamTrigger, IceCreamOverlay);
            }
        }

        void SetParameters (string IngredientTrigger, Sprite Overlay)
        {
            Bowl.IngredientSelector = IngredientTrigger;
            OverlayHelper.OverlaySprite = Overlay;
        }
    }
}