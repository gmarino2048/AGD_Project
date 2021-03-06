﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class UserController : MonoBehaviour
    {
        [Header("Selection Phrase")]
        public int DisplayDelay = 2;
        public PlayByPlayController PlayByPlay;
        public string SelectionPhrase = "I'd better make my next move carefully";

        [Header("User Buttons")]
        public Button Talk;
        public Button Heal;
        public ButtonManager ButtonManager;
        public GameController GameController;

        [Header("User Actions")]
        public UserActions Actions;

        private void Start()
        {
            Talk.gameObject.SetActive(false);
            Heal.gameObject.SetActive(false);
        }

        public IEnumerator UserTurn()
        {
            Actions.UserChoiceMade = false;

            PlayByPlay.DelayFrames = DisplayDelay;
            yield return PlayByPlay.Display(SelectionPhrase);

            Talk.gameObject.SetActive(true);
            GameController.SetHealActive(true);

            yield return new WaitUntil(() => Actions.UserChoiceMade);

            Talk.gameObject.SetActive(false);
            Heal.gameObject.SetActive(false);
            ButtonManager.GoBack();

            yield return PlayByPlay.DisplayUserAction(Actions.Choice);

            yield return new WaitForSeconds(1.5f);
        }
    }
}