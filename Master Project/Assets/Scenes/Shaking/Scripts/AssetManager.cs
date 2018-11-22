using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shaking
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Gravel Assets")]
        public string GravelTrigger = "GravelTrigger";
        public Sprite GravelShaker;
        public Sprite LakebedSprite;
        public List<Sprite> GravelAccumulation;

        [Header("Souls Assets")]
        public string SoulsTrigger = "SoulsTrigger";
        public Sprite SoulsShaker;
        public Sprite ChiliSprite;
        public List<Sprite> SoulsAccumulation;

        [Header("Scene Objects")]
        public SpriteRenderer ShakerSprite;
        public ShakerBehavior Shaker;
        public SpriteRenderer Dish;
        public AccumulationManager Accumulation;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.AquariumGravel:
                        SetParameters(GravelTrigger, GravelShaker, LakebedSprite, GravelAccumulation);
                        break;

                    case IngredientType.CrushedSouls:
                        SetParameters(SoulsTrigger, SoulsShaker, ChiliSprite, SoulsAccumulation);
                        break;

                    default:
                        Debug.LogError("Ingredient not found -- defaulting to Gravel");
                        SetParameters(GravelTrigger, GravelShaker, LakebedSprite, GravelAccumulation);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- defaulting to Gravel");
                SetParameters(GravelTrigger, GravelShaker, LakebedSprite, GravelAccumulation);
            }
        }

        void SetParameters (string TriggerName, Sprite ShakerSet, Sprite DishSprite, List<Sprite> AccumulationSprites) 
        {
            ShakerSprite.sprite = ShakerSet;
            Shaker.StartTrigger = TriggerName;
            Dish.sprite = DishSprite;
            Accumulation.Accumulation = AccumulationSprites;
        }
    }
}
