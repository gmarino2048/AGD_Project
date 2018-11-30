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
        public AudioClip BoneSound;

        [Header("Pepper Assets")]
        public Sprite PepperSprite;
        public Sprite PepperCut;
        public AudioClip PepperSound;

        [Header("GameObjects to Set")]
        public SpriteRenderer ChopObject;
        public SpriteRenderer ChopHelper;
        public SpriteControllerBehavior SpriteController;
        public SFXController SFX;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient)
                {
                    case IngredientType.Bones:
                        SetParameters(BoneSprite, BoneCut, BoneSound);
                        break;

                    case IngredientType.PeculiarPeppers:
                        SetParameters(PepperSprite, PepperCut, PepperSound);
                        break;
                    default:
                        Debug.LogError("Could not find correct ingredient. " +
                                       "Defaulting to Bone.");
                        SetParameters(BoneSprite, BoneCut, BoneSound);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + " -- Defaulting to Bone Parameters");
                SetParameters(BoneSprite, BoneCut, BoneSound);
            }
        }

        void SetParameters (Sprite ObjectSprite, Sprite CutSprite, AudioClip CutSound) 
        {
            ChopObject.sprite = ObjectSprite;
            ChopHelper.sprite = CutSprite;
            SpriteController.CutSprite = CutSprite;
            SFX.KnifeCut = CutSound;
        }
    }
}