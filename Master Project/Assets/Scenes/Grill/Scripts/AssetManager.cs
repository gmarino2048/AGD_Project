using System;
using UnityEngine;

namespace Grill
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Beef Assets")]
        public Sprite UncookedPatty;
        public CookObjectController BeefPrefab;


        [Header("Steak Assets")]
        public Sprite UncookedSteak;
        public CookObjectController SteakPrefab;

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
                        SetParameters(UncookedPatty, BeefPrefab);
                        break;

                    case IngredientType.Steak:
                        SetParameters(UncookedSteak, SteakPrefab);
                        break;

                    default:
                        Debug.LogError("Wrong ingredient type -- defaulting to beef");
                        SetParameters(UncookedPatty, BeefPrefab);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + " -- defaulting to beef");
                SetParameters(UncookedPatty, BeefPrefab);
            }
        }

        void SetParameters(Sprite PlacementHelper, CookObjectController Prefab)
        {
            GrillPlacementHelper.FoodObject = PlacementHelper;
            GrillPlacementController.CookObject = Prefab;
        }
    }
}