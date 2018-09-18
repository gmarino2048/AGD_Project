using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DishScoreManager : MonoBehaviour
{
    private Dictionary<Guid, List<KeyValuePair<IngredientType, float>>> _PREPARED_DISHES = new Dictionary<Guid, List<KeyValuePair<IngredientType, float>>>();

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
            score += Mathf.Pow(dishVector[i], 2);
        }

        score = Mathf.Sqrt(score);

        return score;
    }

    public float GetDesiredValue(Guid monsterId, IngredientType ingredientType)
    {
        // TODO
        return 1;
    }

    public void AddIngredientToDish(Guid monsterId, IngredientType ingredientType, float ingredientScore)
    {
        if (!_PREPARED_DISHES.ContainsKey(monsterId))
        {
            _PREPARED_DISHES.Add(monsterId, new List<KeyValuePair<IngredientType, float>>());
        }

        _PREPARED_DISHES[monsterId].Add(new KeyValuePair<IngredientType, float>(ingredientType, ingredientScore));
    }
}
