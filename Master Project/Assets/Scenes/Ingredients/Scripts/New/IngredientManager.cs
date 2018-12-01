using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

namespace Ingredients
{
    public class IngredientManager : MonoBehaviour
    {
        [Header("Settings")]
        public float Space = 0.25f;
        public UIManager UI;

        public bool GameActive { get; private set; }

        [Header("Prefabs")]
        public PlateController Plate;

        public MonsterData MonsterData { get; private set; }

        public List<PlateController> Plates { get; private set; }
        public List<IngredientController> Ingredients { get; private set; }

        void Start()
        {
            GameActive = false;
            Ingredients = new List<IngredientController>(FindObjectsOfType<IngredientController>());

            MonsterFactory monsterFactory = FindObjectOfType<MonsterFactory>();
            GameNarrativeManager narrativeManager = FindObjectOfType<GameNarrativeManager>();
            DishPreparationManager dishManager = FindObjectOfType<DishPreparationManager>();

            uint numPlates;
            List<IngredientType> ingredientTypes;
            try
            {
                Guid monsterID = narrativeManager.CurrentStage.MonsterID;
                MonsterData = monsterFactory.LoadMonster(monsterID);

                numPlates = (uint) MonsterData.DesiredIngredients.Count;
                ingredientTypes = MonsterData.DesiredIngredients;
            }
            catch (Exception)
            {
                Debug.LogWarning("Could not load persistent objects -- Defaulting to shake");
                numPlates = 3;

                ingredientTypes = new List<IngredientType> 
                { IngredientType.AlgaeSlime, IngredientType.IceCream, IngredientType.AquariumGravel };
            }

            Plates = SetPlates(numPlates, Space);

            Ingredients.ForEach((ingredient) =>
            {
                IngredientType type = ingredient.Type;
                ingredient.InRecipe = ingredientTypes.Contains(type);
                ingredient.Plates = Plates;
            });
        }

        public void Activate()
        {
            GameActive = true;
            Ingredients.ForEach((ingredient) => ingredient.Active = true);
        }

        private void Update()
        {
            bool complete = true;
            Plates.ForEach((plate) =>
            {
                complete &= plate.Used;
            });

            if (complete && GameActive)
            {
                Debug.Log("Scene Complete");
                Ingredients.ForEach((ingredient) => ingredient.Active = false);
                GameActive = false;
                StartCoroutine(UI.EndGame());
            }
        }

        List<PlateController> SetPlates(uint number, float space)
        {
            List<PlateController> plates = new List<PlateController>();

            if (number > 0)
            {
                float width = Plate.Renderer.bounds.size.x;

                float totalWidth = (number * width) + ((number - 1) * space);
                float start = gameObject.transform.position.x - (totalWidth / 2f);

                float yPos = gameObject.transform.position.y;
                float zPos = gameObject.transform.position.z;

                float xPos = start;
                for (int i = 0; i < number; i++)
                {
                    PlateController newPlate = Instantiate(Plate);
                    Vector3 newPosition = new Vector3(xPos +(width / 2), yPos, zPos);
                    newPlate.transform.position = newPosition;

                    plates.Add(newPlate);
                    newPlate.gameObject.transform.parent = gameObject.transform;

                    xPos += width + space;
                }
            }

            return plates;
        }
    }
}