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

        [Header("Monster Responses")]
        public string MissResponse = "Nessie's Attack Missed!";
        public List<string> MissOptions;
        public string AttackDefault = "Nessie Attacks";
        public List<string> AttackOptions;
        public string HealDefault = "Nessie Heals";
        public List<string> HealOptions;

        [Header("Outro Phrases")]
        public string DefaultEnd = "Combat Complete";
        public List<string> Win;
        public List<string> Lose;
        public List<string> Die;

        public void Clear ()
        {
            PlayByPlayText.text = string.Empty;
        }

        public IEnumerator DisplayMonsterAction (MonsterController.MonsterActions action)
        {
            string response = "Monster makes a move";
            List<string> options = new List<string>();

            switch (action)
            {
                case MonsterController.MonsterActions.Hit:
                    response = AttackDefault;
                    options = AttackOptions;
                    break;
                case MonsterController.MonsterActions.Miss:
                    response = MissResponse;
                    options = MissOptions;
                    break;
                case MonsterController.MonsterActions.Heal:
                    response = HealDefault;
                    options = HealOptions;
                    break;
            }

            if (options.Count > 0)
            {
                int item = Mathf.FloorToInt(Random.Range(0f, options.Count - 0.0001f));
                string message = options[item];

                yield return Display(message);
            }
            else yield return Display(response);
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

        public IEnumerator DisplayOutro(GameFlowController.GameEndConditions condition)
        {
            List<string> options = new List<string>();

            switch (condition)
            {
                case GameFlowController.GameEndConditions.Win:
                    options = Win;
                    break;
                case GameFlowController.GameEndConditions.Lose:
                    options = Lose;
                    break;
                case GameFlowController.GameEndConditions.Die:
                    options = Die;
                    break;
            }

            if (options.Count > 0)
            {
                int item = Mathf.FloorToInt(Random.Range(0f, options.Count - 0.0001f));
                string message = options[item];

                yield return Display(message);
            }
            else yield return Display(DefaultEnd);
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