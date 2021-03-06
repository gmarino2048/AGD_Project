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
    private readonly string _DISH_COMPLETE_SCENE_NAME = "FoodScoreScene";

    private const string _CHOPPING_SCENE_NAME = "Chopping";
    private const string _GRILLING_SCENE_NAME = "Grill";
    private const string _MICROWAVE_SCENE_NAME = "Microwaving";
    private const string _SHAKING_SCENE_NAME = "Shaking";
    private const string _STIRRING_SCENE_NAME = "Stirring";
    private const string _COMBAT_SCENE_NAME = "CombatScene";
    private readonly Dictionary<IngredientType, string> _INGREDIENT_SCENES = new Dictionary<IngredientType, string>() {
        {IngredientType.IceCream, _STIRRING_SCENE_NAME},
        {IngredientType.AlgaeSlime, _MICROWAVE_SCENE_NAME},
        {IngredientType.AquariumGravel, _SHAKING_SCENE_NAME},
        {IngredientType.Eggs, _STIRRING_SCENE_NAME},
        {IngredientType.Steak, _GRILLING_SCENE_NAME},
        {IngredientType.Bones, _CHOPPING_SCENE_NAME},
        {IngredientType.CannedMeat, _MICROWAVE_SCENE_NAME},
        {IngredientType.Beans, _MICROWAVE_SCENE_NAME},
        {IngredientType.PeculiarPeppers, _CHOPPING_SCENE_NAME},
        {IngredientType.CrushedSouls, _SHAKING_SCENE_NAME},
        {IngredientType.VoidGoo, _STIRRING_SCENE_NAME},
        {IngredientType.GroundBeef, _GRILLING_SCENE_NAME}
    };

    private readonly Dictionary<IngredientType, string> _INGREDIENT_NAMES = new Dictionary<IngredientType, string>() {
        {IngredientType.IceCream, "Ice cream"},
        {IngredientType.AlgaeSlime, "Algae slime"},
        {IngredientType.AquariumGravel, "Aquarium gravel"},
        {IngredientType.Eggs, "Eggs"},
        {IngredientType.Steak, "Weird steak"},
        {IngredientType.Bones, "Bones"},
        {IngredientType.CannedMeat, "Canned meat"},
        {IngredientType.Beans, "Beans"},
        {IngredientType.PeculiarPeppers, "Peculiar peppers"},
        {IngredientType.CrushedSouls, "Crushed souls"},
        {IngredientType.VoidGoo, "Void goo"},
        {IngredientType.GroundBeef, "Ground beef"},
        {IngredientType.Onions, "Onions"},
        {IngredientType.Cheese, "Cheese"},
        {IngredientType.WhippedCream, "Whipped cream"}
    };

    private CombatInitiator _CombatInitiator;
    private DishScoreManager _DishScoreManager;
    private GameNarrativeManager _GameNarrativeManager;
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

        _GameNarrativeManager = GetComponent<GameNarrativeManager>();
        if (_GameNarrativeManager == null)
        {
            throw new Exception("GameNarrativeManager does not exist on object");
        }

        _MonsterFactory = GetComponent<MonsterFactory>();
        if (_MonsterFactory == null)
        {
            throw new Exception("MonsterFactory does not exist on object");
        }
        
        _IngredientsQueue = null;
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
    /// Gets the display name of an ingredient
    /// </summary>
    /// <param name="ingredientType"></param>
    /// <returns></returns>
    public string GetIngredientDisplayName(IngredientType ingredientType)
    {
        return _INGREDIENT_NAMES[ingredientType];
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

        if (_IngredientsQueue.Any()) // The dish is not completed
        {
            currentIngredient = _IngredientsQueue.Dequeue();
            SceneManager.LoadScene(_INGREDIENT_SCENES[currentIngredient]);
        }
        else
        {
            _IngredientsQueue = null;
            
            var dishScore = _DishScoreManager.ScoreDish(_MonsterID);
            var monster = _MonsterFactory.LoadMonster(_MonsterID);
            
            monster.UpdateAffectionFromDishScore(dishScore);

            SceneManager.LoadScene(_DISH_COMPLETE_SCENE_NAME, LoadSceneMode.Single);
        }
    }
}
