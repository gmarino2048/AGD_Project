using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class MonsterData
    {
        private const float _CONVERSATION_WEIGHT = 0.3f;
        private const float _DISH_WEIGHT = 0.7f;
        private const float _WRONG_INGREDIENT_CHOSEN_AFFECT_CONSTANT = 0.001f;

        private int _WrongIngredientsCount = 0;

        /// <summary>
        /// The name of the monster
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The level to which a monster's {what_should_we_call_it} drops before a fight starts
        /// </summary>
        public float FightThreshold { get; private set; }

        private float _affectionValue;
        /// <summary>
        /// The amount that a monster likes the player. 0 is not at all, 1 is completely.
        /// </summary>
        public float AffectionValue { get; private set; }

        /// <summary>
        /// The ingredients this monster desires
        /// </summary>
        public List<IngredientType> DesiredIngredients { get; private set; }

        public Dictionary<CombatChoice, CombatChoiceStatus> CombatChoices { get; private set; }

        internal MonsterData(string name, float fightThreshold, List<IngredientType> desiredIngredients, Dictionary<CombatChoice, CombatChoiceStatus> combatChoices)
        {
            Name = name;
            FightThreshold = fightThreshold;
            DesiredIngredients = desiredIngredients;
            CombatChoices = combatChoices;

            AffectionValue = 1;
        }

        public void UpdateAffectionFromConversationScore(float conversationScore)
        {
            AffectionValue -= conversationScore * _CONVERSATION_WEIGHT;
        }

        public void UpdateAffectionFromIngredientSelection(IngredientType ingredientType)
        {
            if (DesiredIngredients.Contains(ingredientType)) {
                return;
            }

            _WrongIngredientsCount++;
            AffectionValue -= _WrongIngredientsCount * _WRONG_INGREDIENT_CHOSEN_AFFECT_CONSTANT;
        }

        public void UpdateAffectionFromDishScore(float dishScore)
        {
            AffectionValue -= dishScore * _DISH_WEIGHT;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
