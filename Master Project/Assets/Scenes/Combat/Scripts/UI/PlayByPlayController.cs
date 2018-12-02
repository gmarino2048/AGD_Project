using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Combat
{
    public class PlayByPlayController : MonoBehaviour
    {
        [Header("Play By Play Text")]
        public Text PlayByPlayText;

        [Header("Text Scroll Display")]
        public int DelayFrames = 5;

        [Header("User Dialogue Options")]
        public string Default = "UHHHHHHH...";
        public List<string> Food;
        public List<string> Drink;
        public List<string> Reason;
        public List<string> Flatter;
        public List<string> Coupon;
        public List<string> Beg;
        public List<string> Heal;

        public void Clear ()
        {
            PlayByPlayText.text = string.Empty;
        }

        public IEnumerator DisplayUserAction(UserActions.UserActionChoice choice)
        {
            List<string> options = new List<string>();

            switch (choice)
            {
                case UserActions.UserActionChoice.Food:
                    options = Food;
                    break;
                case UserActions.UserActionChoice.Drink:
                    options = Drink;
                    break;
                case UserActions.UserActionChoice.Reason:
                    options = Reason;
                    break;
                case UserActions.UserActionChoice.Flatter:
                    options = Flatter;
                    break;
                case UserActions.UserActionChoice.Coupon:
                    options = Coupon;
                    break;
                case UserActions.UserActionChoice.Beg:
                    options = Beg;
                    break;
                case UserActions.UserActionChoice.Heal:
                    options = Heal;
                    break;
            }

            if (options.Count > 0)
            {
                int item = Mathf.FloorToInt(Random.Range(0f, options.Count - 0.0001f));
                string message = options[item];

                yield return Display(message);
            }
            else yield return Display(Default);
        }

        public IEnumerator Display (string text)
        {
            int i = 1;
            while (i < text.Length)
            {
                string temp = text.Substring(0, i);
                PlayByPlayText.text = temp;

                
                for (int j = 0; j < DelayFrames; j++) yield return new WaitForEndOfFrame();

                i++;
            }

            PlayByPlayText.text = text;
        }
    }
}