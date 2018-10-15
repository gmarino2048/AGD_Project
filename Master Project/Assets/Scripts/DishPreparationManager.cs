using Monsters;
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
    private readonly string _SUCCESSFUL_SCENE_NAME = "ScenePicker";

    private const string _CHOPPING_SCENE_NAME = "Chopping";
    private const string _GRILLING_SCENE_NAME = "Grill";
    private const string _MICROWAVE_SCENE_NAME = "Microwaving";
    private const string _SHAKING_SCENE_NAME = "Shaking";
    private const string _STIRRING_SCENE_NAME = "Stirring";
    private readonly Dictionary<IngredientType, string> _INGREDIENT_SCENES = new Dictionary<IngredientType, string>() {
        {IngredientType.IceCream, _STIRRING_SCENE_NAME},
        {IngredientType.AlgaeSlime, _MICROWAVE_SCENE_NAME},
        {IngredientType.AquariumGravel, _SHAKING_SCENE_NAME}
    };

    private CombatInitiator _CombatInitiator;
    private DishScoreManager _DishScoreManager;
    private MonsterFactory _MonsterFactory;

    private Guid _MonsterID;
    private Queue<IngredientType> _IngredientsQueue;

    /// <summary>
    /// The current ingredient being prepared
    /// </summary>
    public IngredientType currentIngredient { get; private set; }

    /// <summary>
    /// Just runs because Unity components
    /// </summary>
    void Start()
    {
        _CombatInitiator = GetComponent<CombatInitiator>();
        if (_CombatInitiator == null)
        {
            throw new Exception("CombatInitiator does not exist on object");
        }

        _DishScoreManager = GetComponent<DishScoreManager>();
        if (_DishScoreManager == null)
        {
            throw new Exception("DishScoreManager does not exist on object");
        }

        _MonsterFactory = GetComponent<MonsterFactory>();
        if (_MonsterFactory == null)
        {
            throw new Exception("MonsterFactory does not exist on object");
        }
    }

    /// <summary>
    /// Initiates a queue of ingredients to be made
    /// </summary>
    /// <param name-"monsterId">The monster the dish is being made for</param>
    /// <param name="ingredientsList">Ordered list of ingredients to use</param>
    public void StartPreparingDish(Guid monsterId, List<IngredientType> ingredientsList)
    {
        if (_IngredientsQueue != null)
        {
            throw new Exception("Cannot start preparing a new dish while still preparing one.");
        }

        _MonsterID = monsterId;
        _IngredientsQueue = new Queue<IngredientType>(ingredientsList);
    }

    /// <summary>
    /// Goes to the next scene - whatever that may be!
    /// </summary>
    public void GoToNextScene()
    {
        if (_IngredientsQueue == null)
        {
            throw new Exception("No dish in progress.");
        }

        if (!_IngredientsQueue.Any()) // The dish is completed
        {
            var dishScore = _DishScoreManager.ScoreDish(_MonsterID);
            var monster = _MonsterFactory.LoadMonster(_MonsterID);
            
            monster.UpdateAffectionFromDishScore(dishScore);

            if (monster.AffectionValue <= monster.FightThreshold)
            {
                _CombatInitiator.InitiateCombat(_MonsterID, 1 - monster.AffectionValue);
            }
            else
            {
                SceneManager.LoadScene(_SUCCESSFUL_SCENE_NAME);
            }
            return;
        }
        
        currentIngredient = _IngredientsQueue.Dequeue();
        SceneManager.LoadScene(_INGREDIENT_SCENES[currentIngredient]);
    }
}