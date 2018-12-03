using System;
using UnityEngine;

namespace Grill
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Beef Assets")]
        public Sprite UncookedPatty;
        public CookObjectController BeefPrefab;
        public Vector3 BeefScale;


        [Header("Steak Assets")]
        public Sprite UncookedSteak;
        public CookObjectController SteakPrefab;
        public Vector3 SteakScale;

        [Header("Scene Objects")]
        public PlacementHelper GrillPlacementHelper;
        public PlacementController GrillPlacementController;

        void Awake()
        {
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            try 
            {
                IngredientType ingredient = dishManager.currentIngredient;

                switch (ingredient) 
                {
                    case IngredientType.GroundBeef:
                        SetParameters(UncookedPatty, BeefPrefab, BeefScale);
                        break;

                    case IngredientType.Steak:
                        SetParameters(UncookedSteak, SteakPrefab, SteakScale);
                        break;

                    default:
                        Debug.LogError("Wrong ingredient type -- defaulting to beef");
                        SetParameters(UncookedPatty, BeefPrefab, BeefScale);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + " -- defaulting to beef");
                SetParameters(UncookedPatty, BeefPrefab, BeefScale);
            }
        }

        void SetParameters(Sprite PlacementHelper, CookObjectController Prefab, Vector3 Scale)
        {
            GrillPlacementHelper.FoodObject = PlacementHelper;
            GrillPlacementController.CookObject = Prefab;
            GrillPlacementHelper.Scale = Scale;
        }
    }
}