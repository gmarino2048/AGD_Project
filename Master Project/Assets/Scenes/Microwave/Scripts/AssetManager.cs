using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Slime Assets")]
        public string SlimeTrigger = "Slime";
        public Sprite SlimeOverlay;

        [Header("Meat Assets")]
        public string MeatTrigger = "Meat";
        public Sprite MeatOverlay;

        [Header("Beans Assets")]
        public string BeansTrigger = "Beans";
        public Sprite BeansOverlay;

        [Header("Microwave Assets")]
        public SpriteRenderer MicrowaveOverlay;
        public MicrowaveController MicrowaveController;


        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.AlgaeSlime:
                        SetParameters(SlimeTrigger, SlimeOverlay);
                        break;

                    case IngredientType.CannedMeat:
                        SetParameters(MeatTrigger, MeatOverlay);
                        break;

                    case IngredientType.Beans:
                        SetParameters(BeansTrigger, BeansOverlay);
                        break;

                    default:
                        Debug.LogError("Unexpected Ingredient -- Defaulting to slime");
                        SetParameters(SlimeTrigger, SlimeOverlay);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- Defaulting to Slime");
                SetParameters(SlimeTrigger, SlimeOverlay);
            }
        }

        void SetParameters (string TriggerName, Sprite Overlay)
        {
            MicrowaveOverlay.sprite = Overlay;
            MicrowaveController.StartTrigger = TriggerName;
        }
    }
}