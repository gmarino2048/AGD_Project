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

        public Button BegButton;

        public Button BackButton;

        // Use this for initialization
        void Start()
        {
            UnopenedMenu.gameObject.SetActive(true);
            OpenedMenu.gameObject.SetActive(false);

            TalkButton.onClick.AddListener(OpenTalk);
            BackButton.onClick.AddListener(GoBack);
            PauseButton.onClick.AddListener(Pause);


            FreeFood.onClick.AddListener(Actions.FoodAction);
            OfferDrink.onClick.AddListener(Actions.DrinkAction);
            Reason.onClick.AddListener(Actions.ReasonAction);
            Flatter.onClick.AddListener(Actions.FlatterAction);
            OfferCoupon.onClick.AddListener(Actions.CouponAction);

            HealButton.onClick.AddListener(Actions.HealAction);
            BegButton.onClick.AddListener(Actions.BegAction);
        }

        void OpenTalk ()
        {
            UnopenedMenu.gameObject.SetActive(false);
            OpenedMenu.gameObject.SetActive(true);
        }

        void GoBack ()
        {
            UnopenedMenu.gameObject.SetActive(true);
            OpenedMenu.gameObject.SetActive(false);
        }

        void Pause ()
        {
            //TODO: Put pause information here
        }
    }
}