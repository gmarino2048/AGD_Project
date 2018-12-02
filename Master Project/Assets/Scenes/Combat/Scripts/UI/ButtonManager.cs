using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class ButtonManager : MonoBehaviour
    {
        [Header("Game Objects")]
        public GameController Controller;
        public UserActions Actions;

        [Header("Menu Buttons")]
        public Image UnopenedMenu;

        public Button TalkButton;
        public Button HealButton;
        public Button PauseButton;

        [Header("Talk Buttons")]
        public Image OpenedMenu;

        public Button FreeFood;
        public Button OfferDrink;
        public Button Reason;
        public Button Flatter;
        public Button OfferCoupon;

        public Button Beg;

        public Button Back;

        // Use this for initialization
        void Start()
        {
            UnopenedMenu.gameObject.SetActive(false);
            OpenedMenu.gameObject.SetActive(true);

            FreeFood.onClick.AddListener(Actions.FoodAction);
            OfferDrink.onClick.AddListener(Actions.DrinkAction);
            Reason.onClick.AddListener(Actions.ReasonAction);
            Flatter.onClick.AddListener(Actions.FlatterAction);
            OfferCoupon.onClick.AddListener(Actions.CouponAction);
            HealButton.onClick.AddListener(Actions.HealAction);
            Beg.onClick.AddListener(Actions.BegAction);
        }

        void OpenTalk ()
        {
            OpenedMenu.gameObject.SetActive(true);
            UnopenedMenu.gameObject.SetActive(false);
        }

        void GoBack ()
        {
            UnopenedMenu.gameObject.SetActive(false);
            OpenedMenu.gameObject.SetActive(true);
        }

        void Pause ()
        {
            //TODO: Put pause information here
        }
    }
}