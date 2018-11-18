using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Microwave
{
    public class TimerBehavior : MonoBehaviour
    {
        [Header("Scorekeeper")]
        public ScorekeeperBehavior Scorekeeper;

        [Header("Game Time")]
        public float GameTime = 10f;
        public float TimeRemaining { get; private set; }

        [Header("Time Display")]
        public TextMesh TextMesh;

        [Header("Game Objects")]
        public UIManager WindowManager;
        public MicrowaveController Microwave;
        public string SceneName = "Microwaving";

        public bool NotOpened { get; set; }
        public bool GameActive { get; private set; }
        public bool GameComplete { get; private set; }

        // Use this for initialization
        void Start()
        {
            TimeRemaining = GameTime;

            TextMesh.text = Format(TimeRemaining);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameActive && TimeRemaining > 0)
            {
                TextMesh.text = Format(TimeRemaining);
                TimeRemaining -= Time.deltaTime;
            }
            else if (GameActive && TimeRemaining <= 0)
            {
                TextMesh.text = Format(0f);
                NotOpened = true;
                Stop();
            }
        }

        public void Activate () 
        {
            Microwave.Play();
            GameActive = true;
        }

        public void Stop () 
        {
            GameActive = false;
            Scorekeeper.CalculateScore();
            GameComplete = true;

            StartCoroutine(Flash(0.5f));
            StartCoroutine(EndGame());
        }

        IEnumerator Flash (float interval)
        {
            Color oldColor = TextMesh.color;
            do
            {
                TextMesh.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0f);
                yield return new WaitForSeconds(interval);
                TextMesh.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
                yield return new WaitForSeconds(interval);
            }
            while (SceneManager.GetActiveScene().name == SceneName);
        }

        IEnumerator EndGame ()
        {
            yield return new WaitUntil(() => NotOpened || 
                                       Microwave.MicrowaveAnimator.GetCurrentAnimatorStateInfo(0).IsName(Microwave.FinishedState));
            Microwave.Finish();
            yield return new WaitForSeconds(1);
            yield return WindowManager.EndGame();
        }

        string Format(float timeRemaining) 
        {
            int seconds = Mathf.FloorToInt(timeRemaining);
            string firstPart = seconds.ToString("D2");

            int millis = Mathf.FloorToInt((timeRemaining - seconds) * 100f);
            string secondPart = millis.ToString("D2");

            return firstPart + ":" + secondPart;
        }
    }
}
