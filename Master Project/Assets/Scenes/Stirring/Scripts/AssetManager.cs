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
        public AudioClip IceCreamAudio;

        [Header("Eggs Assets")]
        public string EggsTrigger = "Eggs";
        public Sprite EggsOverlay;
        public AudioClip EggsAudio;

        [Header("Chili Assets")]
        public string ChiliTrigger = "Chili";
        public Sprite ChiliOverlay;
        public AudioClip ChiliAudio;

        [Header("Scene Objects")]
        public BowlBehavior Bowl;
        public SpoonBehavior OverlayHelper;
        public SFXManager SFX;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.IceCream:
                        SetParameters(IceCreamTrigger, IceCreamOverlay, IceCreamAudio);
                        break;

                    case IngredientType.Eggs:
                        SetParameters(EggsTrigger, EggsOverlay, EggsAudio);
                        break;

                    case IngredientType.VoidGoo:
                        SetParameters(ChiliTrigger, ChiliOverlay, ChiliAudio);
                        break;

                    default:
                        Debug.LogError("Incorrect Ingredient -- defaulting to ice cream");
                        SetParameters(IceCreamTrigger, IceCreamOverlay, IceCreamAudio);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- defaulting to ice cream");
                SetParameters(IceCreamTrigger, IceCreamOverlay, IceCreamAudio);
            }
        }

        void SetParameters (string IngredientTrigger, Sprite Overlay, AudioClip Clip)
        {
            Bowl.IngredientSelector = IngredientTrigger;
            OverlayHelper.OverlaySprite = Overlay;
            SFX.StirringAudio = Clip;
        }
    }
}