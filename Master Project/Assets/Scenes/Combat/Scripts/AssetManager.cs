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

        [Header("Monster Meter")]
        public InfoBarController Manager;
        public SFXController SFX;

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

            Manager.StartPercentage = combatInitiator.InitialManagerMeterValue * 100;
        }

		private void SetNessie(MonsterData monster, GameFlowController gameFlowController, PlayByPlayController playByPlayController)
        {
			nessieAnimator.gameObject.SetActive(true);
			gameFlowController.MonsterAnimator = nessieAnimator;
			gameFlowController.StartQuip1 = "I can't believe I managed to mess up a MILKSHAKE.";
			gameFlowController.StartQuip2 = "...It appears the tides have turned.";

			playByPlayController.Beg = new List<string> {
				"Please, I have so much to live for."
			};
			playByPlayController.Coupon = new List<string> {
				"Here's a voucher for one free meal!"
			};
			playByPlayController.Drink = new List<string> {
				"Can I offer you a free Qennies quality drink?",
                "It's on the house, drink 'til your heart's content!",
                "Wash down your sorrows with this free drink!",
                "We should drink to this crappy job of mine",
                "I'm thirsty, what about you?"
			};
			playByPlayController.Flatter = new List<string> {
				"You're not on our menu, but you look like a snack to me.",
                "Is it hot in here, or is it just you?",
                "Stay here any longer, and I may just have to quit and leave with you.",
                "Know what's on the menu? Me-n-U.",
                "Shall we dine together tonight? The bill's on me."
			};
			playByPlayController.Food = new List<string> {
				"Can I offer you a free Qennies kids meal on your next visit?",
                "Food of this quality should be free, here, take it.",
                "Everyone loves free food, right?",
                "Would you like your food...free?",
                "You drive a hard bargain, what about free food?"
			};
            playByPlayController.Heal = new List<string> {
                "They don't like my cooking? Guess I'll eat it",
                "An apple a day keeps the monsters away!",
                "These leftovers have the boss' name on them... Oh well.",
                "Some take out to take you DOWN.",
                "Who ate half the sandwich I was saving!?!",
                "Leftovers. YES!"
			};
			playByPlayController.Reason = new List<string> {
				"Wait, if you hurt me, I'll sue!",
                "My boss is in "
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
			gameFlowController.StartQuip1 = "Heh...hehe...nice doggie?";
			gameFlowController.StartQuip2 = "...I guess I'm in the doghouse now.";

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
			gameFlowController.StartQuip1 = "...Fuck.";
			gameFlowController.StartQuip2 = "This is gonna hurt.";

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