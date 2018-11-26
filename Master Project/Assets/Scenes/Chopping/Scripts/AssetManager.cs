using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chopping
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Bone Assets")]
        public Sprite BoneSprite;
        public Sprite BoneCut;

        [Header("Pepper Assets")]
        public Sprite PepperSprite;
        public Sprite PepperCut;

        [Header("GameObjects to Set")]
        public SpriteRenderer ChopObject;
        public SpriteRenderer ChopHelper;
        public SpriteControllerBehavior SpriteController;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.Bones:
                        SetParameters(BoneSprite, BoneCut);
                        break;

                    case IngredientType.PeculiarPeppers:
                        SetParameters(PepperSprite, PepperCut);
                        break;
                    default:
                        Debug.LogError("Could not find correct ingredient. " +
                                       "Defaulting to Bone.");
                        SetParameters(BoneSprite, BoneCut);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + " -- Defaulting to Bone Parameters");
                SetParameters(BoneSprite, BoneCut);
            }
        }

        void SetParameters (Sprite ObjectSprite, Sprite CutSprite) 
        {
            ChopObject.sprite = ObjectSprite;
            ChopHelper.sprite = CutSprite;
            SpriteController.CutSprite = CutSprite;
        }
    }
}