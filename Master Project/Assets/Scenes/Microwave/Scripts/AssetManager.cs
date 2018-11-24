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
        public Sprite SlimeStart;
        public Sprite SlimeClosed;
        public Sprite SlimeOpen;
        public AudioClip SlimeBubble;

        [Header("Meat Assets")]
        public string MeatTrigger = "Meat";
        public Sprite MeatStart;
        public Sprite MeatClosed;
        public Sprite MeatOpen;
        public AudioClip MeatBang;

        [Header("Beans Assets")]
        public string BeansTrigger = "Beans";
        public Sprite BeansStart;
        public Sprite BeansClosed;
        public Sprite BeansOpen;
        public AudioClip BeansBang;

        [Header("Microwave Assets")]
        public MicrowaveController MicrowaveController;
        public SFXController SFX;


        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.AlgaeSlime:
                        SetParameters(SlimeTrigger, SlimeStart, SlimeClosed, SlimeOpen, SlimeBubble);
                        break;

                    case IngredientType.CannedMeat:
                        SetParameters(MeatTrigger, MeatStart, MeatClosed, MeatOpen, MeatBang);
                        break;

                    case IngredientType.Beans:
                        SetParameters(BeansTrigger, BeansStart, BeansClosed, BeansOpen, BeansBang);
                        break;

                    default:
                        Debug.LogError("Unexpected Ingredient -- Defaulting to slime");
                        SetParameters(SlimeTrigger, SlimeStart, SlimeClosed, SlimeOpen, SlimeBubble);
                        break;
                }
            }
            catch (Exception ex) 
            {
                Debug.LogError(ex.Message + " -- Defaulting to Slime");
                SetParameters(SlimeTrigger, SlimeStart, SlimeClosed, SlimeOpen, SlimeBubble);
            }
        }

        void SetParameters (string TriggerName, Sprite OverlayStart, Sprite OverlayClosed, Sprite OverlayOpen, AudioClip Extra)
        {
            MicrowaveController.StartImage = OverlayStart;
            MicrowaveController.ClosedImage = OverlayClosed;
            MicrowaveController.OpenImage = OverlayOpen;
            MicrowaveController.StartTrigger = TriggerName;
            SFX.MicrowaveExtra = Extra;
        }
    }
}