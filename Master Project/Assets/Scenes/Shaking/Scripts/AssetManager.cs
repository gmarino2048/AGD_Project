using System;
using UnityEngine;

namespace Shaking
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Gravel Assets")]
        public string GravelTrigger = "GravelTrigger";
        public Sprite GravelShaker;
        public Sprite LakebedSprite;

        [Header("Souls Assets")]
        public string SoulsTrigger = "SoulsTrigger";
        public Sprite SoulsShaker;
        public Sprite ChiliSprite;

        [Header("Scene Objects")]
        public SpriteRenderer ShakerSprite;
        public ShakerBehavior Shaker;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.AquariumGravel:
                        SetParameters(GravelTrigger, GravelShaker);
                        break;

                    case IngredientType.CrushedSouls:
                        SetParameters(SoulsTrigger, SoulsShaker);
                        break;

                    default:
                        Debug.LogError("Ingredient not found -- defaulting to Gravel");
                        SetParameters(GravelTrigger, GravelShaker);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- defaulting to Gravel");
                SetParameters(GravelTrigger, GravelShaker);
            }
        }

        void SetParameters (string TriggerName, Sprite ShakerSet) 
        {
            ShakerSprite.sprite = ShakerSet;
            Shaker.StartTrigger = TriggerName;
        }
    }
}
