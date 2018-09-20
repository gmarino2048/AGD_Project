using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used to manage dish scores and track them between scenes
/// </summary>
public class DishScoreManager : MonoBehaviour
{
    private Dictionary<Guid, List<KeyValuePair<IngredientType, float>>> _PREPARED_DISHES = new Dictionary<Guid, List<KeyValuePair<IngredientType, float>>>();

    /// <summary>
    /// Scores the currently stored feature vector representing the success on each ingredient for the dish.
    /// </summary>
    /// <param name="monsterId">The ID of the monster in question</param>
    /// <returns>A real number representing the score. NOTE: This score is like a golf score - the lower the score, the better. Perfect is 0.</returns>
    public float ScoreDish(Guid monsterId)
    {
        if (!_PREPARED_DISHES.ContainsKey(monsterId))
        {
            return float.MinValue;
        }

        var dishVector = _PREPARED_DISHES[monsterId].Select(kvp => kvp.Value).ToArray();

        var score = 0f;

        for (var i = 0; i < dishVector.Length; i++)
        {
            // TODO Check if the monster actually wanted the ingredient, otherwise do 1 - x.
            score += Mathf.Pow(dishVector[i], 2);
        }

        score = Mathf.Sqrt(score);

        return score;
    }

    /// <summary>
    /// Gets the desired value, whatever this value may mean, for a specific ingredient type as per a specific monster's preferences.
    /// </summary>
    /// <param name="monsterId">The ID of the monster in question</param>
    /// <param name="ingredientType">The ingredient type in question</param>
    /// <returns>A real number representing the desired value for the particular ingredient</returns>
    public float GetDesiredValue(Guid monsterId, IngredientType ingredientType)
    {
        // TODO
        return 1;
    }

    /// <summary>
    /// Adds an ingredient to a dish that is being prepared for a particular monster. The score should be relative to the desired value for the particular ingredient type.
    /// </summary>
    /// <param name="monsterId">The ID of the monster in question</param>
    /// <param name="ingredientType">The ingredient type in question</param>
    /// <param name="ingredientScore">The score, as judged by the minigame for the particular ingredient and based on the desired value</param>
    public void AddIngredientToDish(Guid monsterId, IngredientType ingredientType, float ingredientScore)
    {
        if (!_PREPARED_DISHES.ContainsKey(monsterId))
        {
            _PREPARED_DISHES.Add(monsterId, new List<KeyValuePair<IngredientType, float>>());
        }

        _PREPARED_DISHES[monsterId].Add(new KeyValuePair<IngredientType, float>(ingredientType, ingredientScore));
    }
}
