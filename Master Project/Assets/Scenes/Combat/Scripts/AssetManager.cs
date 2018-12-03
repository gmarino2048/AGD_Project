using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

namespace Combat
{
    public class AssetManager : MonoBehaviour
    {

        [Header("Nessie Assets")]
        private const string _NESSIE_NAME = "Nessie";
        public Animator nessieAnimator;

        [Header("Cerberus Assets")]
        private const string _CERBERUS_NAME = "Cerberus";
        public Animator cerberusAnimator;

        [Header("[REDACTED] Assets")]
        private const string _REDACTED_NAME = "[REDACTED]";
        public Animator redactedAnimator;

        void Awake()
        {
			var combatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
			if (combatInitiator == null) return;

            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();

			var gameFlowController = GameObject.FindObjectOfType<GameFlowController>();
			var playByPlayController = GameObject.FindObjectOfType<PlayByPlayController>();
			var userActions = GameObject.FindObjectOfType<UserActions>();

			var monster = monsterFactory.LoadMonster(combatInitiator.MonsterID);

			switch (monster.Name)
			{
				case _NESSIE_NAME:
					SetNessie(monster, gameFlowController, playByPlayController);
					break;
				case _CERBERUS_NAME:
					SetCerberus(monster, gameFlowController, playByPlayController);
					break;
				case _REDACTED_NAME:
					SetRedacted(monster, gameFlowController, playByPlayController);
					break;
			}

			userActions.Beg = monster.CombatChoices[CombatChoice.Beg].Power;
			userActions.Flatter = monster.CombatChoices[CombatChoice.Flatter].Power;
			userActions.FreeFood = monster.CombatChoices[CombatChoice.FreeFood].Power;
			userActions.OfferCoupon = monster.CombatChoices[CombatChoice.Coupon].Power;
			userActions.OfferDrink = monster.CombatChoices[CombatChoice.OfferDrink].Power;
			userActions.Reason = monster.CombatChoices[CombatChoice.Reason].Power;
        }

		private void SetNessie(MonsterData monster, GameFlowController gameFlowController, PlayByPlayController playByPlayController)
        {
			nessieAnimator.gameObject.SetActive(true);
			gameFlowController.MonsterAnimator = nessieAnimator;
			gameFlowController.StartQuip1 = "Oh no! I messed up.";
			gameFlowController.StartQuip2 = "I'd better talk her down...";
			gameFlowController.EndPhrases = new List<string> {
				"Thank goodness, it's over"
			};

			playByPlayController.Beg = new List<string> {
				"Please, I have so much to live for."
			};
			playByPlayController.Coupon = new List<string> {
				"Here's a voucher for one free meal!"
			};
			playByPlayController.Drink = new List<string> {
				"Have a free drink, on the house."
			};
			playByPlayController.Flatter = new List<string> {
				"You look really hot when you're mad."
			};
			playByPlayController.Food = new List<string> {
				"Can I offer you some free food?", "I'll make it again! Better this time!"
			};
			playByPlayController.Heal = new List<string> {
				"Now where was that first aid kit again?"
			};
			playByPlayController.Reason = new List<string> {
				"Don't you think you're being irrational here?"
			};
			playByPlayController.AttackOptions = new List<string> {
				_NESSIE_NAME + " sends a huge splash!"
			};
			playByPlayController.HealOptions = new List<string> {
				_NESSIE_NAME + " healed!"
			};
			playByPlayController.MissOptions = new List<string> {
				_NESSIE_NAME + "'s attack missed!"
			};
        }

        private void SetCerberus(MonsterData monster, GameFlowController gameFlowController, PlayByPlayController playByPlayController)
        {
			cerberusAnimator.gameObject.SetActive(true);
			gameFlowController.MonsterAnimator = cerberusAnimator;
			gameFlowController.StartQuip1 = "Oh no! I messed up.";
			gameFlowController.StartQuip2 = "I'd better talk her down...";
			gameFlowController.EndPhrases = new List<string> {
				"Thank goodness, it's over"
			};

			playByPlayController.Beg = new List<string> {
				"Please, I have so much to live for."
			};
			playByPlayController.Coupon = new List<string> {
				"Here's a voucher for one free meal!"
			};
			playByPlayController.Drink = new List<string> {
				"Have a free drink, on the house."
			};
			playByPlayController.Flatter = new List<string> {
				"You look really hot when you're mad."
			};
			playByPlayController.Food = new List<string> {
				"Can I offer you some free food?", "I'll make it again! Better this time!"
			};
			playByPlayController.Heal = new List<string> {
				"Now where was that first aid kit again?"
			};
			playByPlayController.Reason = new List<string> {
				"Don't you think you're being irrational here?"
			};
			playByPlayController.AttackOptions = new List<string> {
				_CERBERUS_NAME + " attacked!"
			};
			playByPlayController.HealOptions = new List<string> {
				_CERBERUS_NAME + " healed!"
			};
			playByPlayController.MissOptions = new List<string> {
				_CERBERUS_NAME + "'s attack missed!"
			};
        }

        private void SetRedacted(MonsterData monster, GameFlowController gameFlowController, PlayByPlayController playByPlayController)
        {
			redactedAnimator.gameObject.SetActive(true);
			gameFlowController.MonsterAnimator = redactedAnimator;
			gameFlowController.StartQuip1 = "Oh no! I messed up.";
			gameFlowController.StartQuip2 = "I'd better talk her down...";
			gameFlowController.EndPhrases = new List<string> {
				"Thank goodness, it's over"
			};

			playByPlayController.Beg = new List<string> {
				"Please, I have so much to live for."
			};
			playByPlayController.Coupon = new List<string> {
				"Here's a voucher for one free meal!"
			};
			playByPlayController.Drink = new List<string> {
				"Have a free drink, on the house."
			};
			playByPlayController.Flatter = new List<string> {
				"You look really hot when you're mad."
			};
			playByPlayController.Food = new List<string> {
				"Can I offer you some free food?", "I'll make it again! Better this time!"
			};
			playByPlayController.Heal = new List<string> {
				"Now where was that first aid kit again?"
			};
			playByPlayController.Reason = new List<string> {
				"Don't you think you're being irrational here?"
			};
			playByPlayController.AttackOptions = new List<string> {
				_REDACTED_NAME + " attacked!"
			};
			playByPlayController.HealOptions = new List<string> {
				_REDACTED_NAME + " healed!"
			};
			playByPlayController.MissOptions = new List<string> {
				_REDACTED_NAME + "'s attack missed!"
			};
        }
    }
}