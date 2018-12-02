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

        [Header("Info Displays")]
        public InfoBarController Health;
        public InfoBarController Manager;

        [Header("Image Covers")]
        public Image HealCover;
        public Button HealButton;
        public Text HealText;

        public Image BegCover;
        public Button BegButton;

        private void Start()
        {
            ReductionCounter = new List<int>(7);
            HealText.text = "(4)";
        }

        public void FoodAction ()
        {
            Manager.SetValue(Manager.Percentage - FreeFood);
            FreeFood *= ReductionCounter[0] < 5 ? ReductionScaler : 1;
            ReductionCounter[0]++;
        }

        public void DrinkAction ()
        {
            Manager.SetValue(Manager.Percentage - OfferDrink);
            FreeFood *= ReductionCounter[1] < 5 ? ReductionScaler : 1;
            ReductionCounter[1]++;
        }

        public void ReasonAction()
        {
            Manager.SetValue(Manager.Percentage - Reason);
            FreeFood *= ReductionCounter[2] < 5 ? ReductionScaler : 1;
            ReductionCounter[2]++;
        }

        public void FlatterAction()
        {
            Manager.SetValue(Manager.Percentage - Flatter);
            FreeFood *= ReductionCounter[3] < 5 ? ReductionScaler : 1;
            ReductionCounter[3]++;
        }

        public void CouponAction()
        {
            Manager.SetValue(Manager.Percentage - Flatter);
            FreeFood *= ReductionCounter[4] < 5 ? ReductionScaler : 1;
            ReductionCounter[4]++;
        }

        public void BegAction()
        {
            Manager.SetValue(Manager.Percentage - Beg);
            ReductionCounter[5]++;

            BegCover.gameObject.SetActive(true);
            BegButton.gameObject.SetActive(false);
        }

        public void HealAction ()
        {
            if (ReductionCounter[6] < 4)
            {
                Manager.SetValue(Health.Percentage + Heal);
                ReductionCounter[6]++;

                HealText.text = "(" + (4 - ReductionCounter[6]).ToString() + ")";
            }
            else
            {
                HealCover.gameObject.SetActive(true);
                BegButton.gameObject.SetActive(false);
                HealText.gameObject.SetActive(false);
            }
        }
    }
}