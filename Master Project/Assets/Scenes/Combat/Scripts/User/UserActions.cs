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
        public bool UserChoiceMade { get; set; }

        [Header("Game Contoller")]
        public GameController Controller;

        [Header("Image Covers")]
        public Image HealCover;
        public Button HealButton;
        public Text HealText;

        public Image BegCover;
        public Button BegButton;

        public enum UserActionChoice
        {
            Food,
            Drink,
            Reason,
            Flatter,
            Coupon,
            Beg,
            Heal
        }

        public UserActionChoice Choice { get; private set; }

        private void Start()
        {
            ReductionCounter = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0 });
            HealText.text = "(4)";
        }

        public void FoodAction ()
        {
            Controller.DamageMonster(FreeFood);
            FreeFood *= ReductionCounter[0] < 5 ? ReductionScaler : 1;
            ReductionCounter[0]++;

            UserChoiceMade = true;
            Choice = UserActionChoice.Food;
        }

        public void DrinkAction ()
        {
            Controller.DamageMonster(OfferDrink);
            OfferDrink *= ReductionCounter[1] < 5 ? ReductionScaler : 1;
            ReductionCounter[1]++;

            UserChoiceMade = true;
            Choice = UserActionChoice.Drink;
        }

        public void ReasonAction()
        {
            Controller.DamageMonster(Reason);
            Reason *= ReductionCounter[2] < 5 ? ReductionScaler : 1;
            ReductionCounter[2]++;

            UserChoiceMade = true;
            Choice = UserActionChoice.Reason;
        }

        public void FlatterAction()
        {
            Controller.DamageMonster(Flatter);
            Flatter *= ReductionCounter[3] < 5 ? ReductionScaler : 1;
            ReductionCounter[3]++;

            UserChoiceMade = true;
            Choice = UserActionChoice.Flatter;
        }

        public void CouponAction()
        {
            Controller.DamageMonster(OfferCoupon);
            OfferCoupon *= ReductionCounter[4] < 5 ? ReductionScaler : 1;
            ReductionCounter[4]++;

            UserChoiceMade = true;
            Choice = UserActionChoice.Coupon;
        }

        public void BegAction()
        {
            Controller.DamageMonster(Beg);
            ReductionCounter[5]++;

            Controller.SetBegActive(false);
            UserChoiceMade = true;
            Choice = UserActionChoice.Beg;
        }

        public void HealAction ()
        {
            if (ReductionCounter[6] < 3)
            {
                Controller.HealPlayer(Heal);
                ReductionCounter[6]++;

                HealText.text = "(" + (4 - ReductionCounter[6]).ToString() + ")";
            }
            else if (ReductionCounter[6] == 3)
            {
                Controller.HealPlayer(Heal);
                ReductionCounter[6]++;

                HealText.text = "(" + (4 - ReductionCounter[6]).ToString() + ")";

                Controller.SetHealActive(false);
            }

            UserChoiceMade = true;
            Choice = UserActionChoice.Heal;
        }
    }
}