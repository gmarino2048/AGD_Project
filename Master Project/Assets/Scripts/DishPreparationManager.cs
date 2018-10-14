using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to go between cooking scenes appropriately
/// </summary>
public class DishPreparationManager : MonoBehaviour
{
    private readonly string _POST_INGREDIENTS_SCENE = "IngredientsScene";

    private const string _CHOPPING_SCENE_NAME = "Chopping";
    private const string _GRILLING_SCENE_NAME = "Grill";
    private const string _MICROWAVE_SCENE_NAME = "Microwave_Goldberg";
    private const string _SHAKING_SCENE_NAME = "Shaking";
    private const string _STIRRING_SCENE_NAME = "Stiring";
    private readonly Dictionary<IngredientType, string> _INGREDIENT_SCENES = new Dictionary<IngredientType, string>() {
        {IngredientType.IceCream, _STIRRING_SCENE_NAME},
        {IngredientType.AlgaeSlime, _MICROWAVE_SCENE_NAME},
        {IngredientType.AquariumGravel, _SHAKING_SCENE_NAME}
    };

    private Queue<IngredientType> _IngredientsQueue;
    private IngredientType _CurrentIngredient;

    public void StartPreparingDish(List<IngredientType> ingredientsList)
    {
        if (_IngredientsQueue != null)
        {
            throw new Exception("Cannot start preparing a new dish while still preparing one.");
        }

        _IngredientsQueue = new Queue<IngredientType>(ingredientsList);
    }

    public void GoToNextScene()
    {
        if (_IngredientsQueue == null)
        {
            throw new Exception("No dish in progress.");
        }

        if (!_IngredientsQueue.Any()) // The dish is completed
        {
            SceneManager.LoadScene(_POST_INGREDIENTS_SCENE);
            return;
        }
        
        _CurrentIngredient = _IngredientsQueue.Dequeue();
        SceneManager.LoadScene(_INGREDIENT_SCENES[_CurrentIngredient]);
    }
}