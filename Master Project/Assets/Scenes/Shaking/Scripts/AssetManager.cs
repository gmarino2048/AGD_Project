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
        public GameObject LakebedShake;
        public List<Sprite> GravelAccumulation;

        [Header("Souls Assets")]
        public string SoulsTrigger = "SoulsTrigger";
        public Sprite SoulsShaker;
        public GameObject Chili;
        public List<Sprite> SoulsAccumulation;

        [Header("Scene Objects")]
        public SpriteRenderer ShakerSprite;
        public ShakerBehavior Shaker;
        public SpriteRenderer Dish;
        public AccumulationManager Accumulation;

        void Awake()
        {
            LakebedShake.SetActive(false);
            Chili.SetActive(false);
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.AquariumGravel:
                        SetParameters(GravelTrigger, GravelShaker, LakebedShake, GravelAccumulation);
                        break;

                    case IngredientType.CrushedSouls:
                        SetParameters(SoulsTrigger, SoulsShaker, Chili, SoulsAccumulation);
                        break;

                    default:
                        Debug.LogError("Ingredient not found -- defaulting to Gravel");
                        SetParameters(GravelTrigger, GravelShaker, LakebedShake, GravelAccumulation);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- defaulting to Gravel");
                SetParameters(GravelTrigger, GravelShaker, LakebedShake, GravelAccumulation);
            }
        }

        void SetParameters (string TriggerName, Sprite ShakerSet, GameObject DishObject, List<Sprite> AccumulationSprites) 
        {
            ShakerSprite.sprite = ShakerSet;
            Shaker.StartTrigger = TriggerName;
            DishObject.SetActive(true);
            Accumulation.Accumulation = AccumulationSprites;
        }
    }
}
