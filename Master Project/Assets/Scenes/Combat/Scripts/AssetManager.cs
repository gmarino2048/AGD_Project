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
        public AudioClip NessieMusic;

        [Header("Cerberus Assets")]
        private const string _CERBERUS_NAME = "Cerberus";
        public Animator cerberusAnimator;
        public AudioClip CerberusMusic;

        [Header("[REDACTED] Assets")]
        private const string _REDACTED_NAME = "[REDACTED]";
        public Animator redactedAnimator;
        public AudioClip RedactedMusic;

        [Header("Monster Meter")]
        public InfoBarController Manager;
        public MusicController Music;
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
                    nessieAnimator.gameObject.SetActive(false);
					SetCerberus(monster, gameFlowController, playByPlayController);
					break;
				case _REDACTED_NAME:
                    nessieAnimator.gameObject.SetActive(false);
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

            Music.Music = NessieMusic;

            SetCommonDialogue(playByPlayController);
        }

        private void SetCerberus(MonsterData monster, GameFlowController gameFlowController, PlayByPlayController playByPlayController)
        {
			cerberusAnimator.gameObject.SetActive(true);
			gameFlowController.MonsterAnimator = cerberusAnimator;
			gameFlowController.StartQuip1 = "Heh...hehe...nice doggie?";
			gameFlowController.StartQuip2 = "...I guess I'm in the doghouse now.";

            Music.Music = CerberusMusic;

            SetCommonDialogue(playByPlayController);
        }

        private void SetRedacted(MonsterData monster, GameFlowController gameFlowController, PlayByPlayController playByPlayController)
        {
			redactedAnimator.gameObject.SetActive(true);
			gameFlowController.MonsterAnimator = redactedAnimator;
			gameFlowController.StartQuip1 = "...Fuck.";
			gameFlowController.StartQuip2 = "This is gonna hurt.";

            Music.Music = RedactedMusic;

            SetCommonDialogue(playByPlayController);
        }

        private void SetCommonDialogue (PlayByPlayController playByPlayController)
        {
            playByPlayController.Beg = new List<string> {
                "I need this job to pay off my student debt.",
                "Without this job I can't pay my rent!",
                "Please! Nobody else will hire me.",
                "This is my first job, I can't lose it already!",
                "I have nothing else going for me in life, please, not like this."
            };
            playByPlayController.Coupon = new List<string> {
                "Would you like a Qennies coupon for use on your next visit?",
                "Wait! There's a QR code code on the back of your drink for half off on your next meal!",
                "Can I offer you a lifetime coupon book?",
                "instead of taking my arm and leg, here's a coupon!"
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
                "My boss is in the back, he wouldn't be happy to see an upset customer.",
                "I'm the only worker here, at least let me live until the end of my shift.",
                "There are cameras in here! ...Somewhere...I think.",
                "I can't become a better employee if you get me fired!"
            };
            playByPlayController.AttackOptions = new List<string> {
                "THAT COUNTER CAN'T PROTECT YOU NOW!",
                "I CANT WAIT TO SINK MY TEETH INTO YOU!",
                "I'M SURE YOU'LL TASTE BETTER THAN THE FOOD YOU SERVED ME!",
                "PREPARE TO DIE.",
                "TRY AND DODGE THIS ONE, HUMAN!",
                "DID YOU THINK YOU COULD HIDE?",
                "TIME TO TEAR INTO SOME FINE DINING!"
            };
            playByPlayController.HealOptions = new List<string> {
                "FEELING REFRESHED! TIME FOR YOU TO DIE!",
                "THE LONGER I LOOK AT YOU, THE MADDER I GET!",
                "I INSIST ON TELLING YOUR MANAGER ABOUT HOW BAD YOU ARE AT THIS.",
                "YOUR ATTEMPTS TO APPEASE ME ARE HORRIBLE!",
                "WHY DO YOU STILL TRY, HUMAN?",
                "YOUR MANAGER WILL BE HEARING ABOUT THIS."
            };
            playByPlayController.MissOptions = new List<string> {
                "HOW DID I MISS THIS SAD, PATHETIC HUMAN?",
                "WHY ARE THE FLOORS SO GREASY? IT'S HARD TO AIM!",
                "THE SMELL COMING FROM THE KITCHEN... I CAN'T THINK STRAIGHT.",
                "THE SAD DINER WORKER CAN ACTUALLY DODGE CORRECTLY?",
                "STOP MOVING SO MUCH!!!",
                "QUIT HIDING BEHIND THAT COUNTER!!",
                "JUST WAIT UNTIL IT'S MY TURN AGAIN, YOU'RE D E A D"
            };
            playByPlayController.Win = new List<string>
            {
                "Phew... glad that's over. I better get overtime for this.",
                "Thank you! Come again!",
                "If I'm not employee of the month I'm gonna be pissed.",
                "And they didn't even leave a tip...",
                "At least they didn't ask for a refund."
            };
            playByPlayController.Lose = new List<string>
            {
                "Well, I had a nice run working here. HELLO UNEMPLOYMENT!",
                "MANAGER: Turn in your badge and gun. Where did you even get those?",
                "At least I've always got showbiz to fall back on...",
                "Do I still get to keep my employee discount?",
                "But how will I pay for my fidget spinners???",
                "Fine, I didn't wanna date you anyway"
                // OwO pwease don't fiwe me mistew managew
            };
            playByPlayController.Die = new List<string>
            {
                "I...didn't..get paid enough...for...this...",
                "SOMEBODY HELP ME PLEASE!!!",
                "Well, guess I'll die.",
                "Must... move... toward... the Light...",
                "At least I won't have to pay for retirement.",
                "The IRS can't hurt me now!"
            };
        }
    }
}