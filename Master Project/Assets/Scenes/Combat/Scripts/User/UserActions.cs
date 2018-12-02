using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class UserActions : MonoBehaviour {

        [Header("User Action Values")]
        public float FreeFood;
        public float OfferDrink;
        public float Reason;
        public float Flatter;
        public float OfferCoupon;
        public float Beg = 30;
        public float Heal = 25;

        List<int> ReductionCounter;
        public float ReductionScaler;

        [Header("Game Contoller")]
        public GameController Controller;

        [Header("Image Covers")]
        public Image HealCover;
        public Button HealButton;
        public Text HealText;

        public Image BegCover;
        public Button BegButton;

        private void Start()
        {
            ReductionCounter = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0 });
            HealText.text = "(4)";
        }

        public void FoodAction ()
        {
            Controller.DamageMonster(FreeFood, GameController.LastEvent.UserAttack);
            FreeFood *= ReductionCounter[0] < 5 ? ReductionScaler : 1;
            ReductionCounter[0]++;
        }

        public void DrinkAction ()
        {
            Controller.DamageMonster(OfferDrink, GameController.LastEvent.UserAttack);
            FreeFood *= ReductionCounter[1] < 5 ? ReductionScaler : 1;
            ReductionCounter[1]++;
        }

        public void ReasonAction()
        {
            Controller.DamageMonster(Reason, GameController.LastEvent.UserAttack);
            FreeFood *= ReductionCounter[2] < 5 ? ReductionScaler : 1;
            ReductionCounter[2]++;
        }

        public void FlatterAction()
        {
            Controller.DamageMonster(Flatter, GameController.LastEvent.UserAttack);
            FreeFood *= ReductionCounter[3] < 5 ? ReductionScaler : 1;
            ReductionCounter[3]++;
        }

        public void CouponAction()
        {
            Controller.DamageMonster(OfferCoupon, GameController.LastEvent.UserAttack);
            FreeFood *= ReductionCounter[4] < 5 ? ReductionScaler : 1;
            ReductionCounter[4]++;
        }

        public void BegAction()
        {
            Controller.DamageMonster(Beg, GameController.LastEvent.UserAttack);
            ReductionCounter[5]++;

            Controller.SetBegActive(false);
        }

        public void HealAction ()
        {
            if (ReductionCounter[6] < 4)
            {
                Controller.HealPlayer(Heal, GameController.LastEvent.UserHeal);
                ReductionCounter[6]++;

                HealText.text = "(" + (4 - ReductionCounter[6]).ToString() + ")";
            }
            else
            {
                Controller.SetHealActive(false);
            }
        }
    }
}