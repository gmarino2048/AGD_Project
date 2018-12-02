using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class GameFlowController : MonoBehaviour
    {
        [Header("Play By Play Controller")]
        public PlayByPlayController PlayByPlay;

        [Header("Intro Phrases")]
        public string StartQuip1 = "Shit... I messed up...";
        public string StartQuip2 = "I'd better talk her down.";

        [Header("Intro Animation")]
        public Animator Monster;
        public string StartTrigger = "start";
        public string ContinueTrigger = "continue";

        [Header("Overlay Manager")]
        public OverlayController Overlay;

        [Header("Music Controller")]
        public MusicController GameMusic;

        [Header("Health Bars")]
        public GameObject HealthMeters;
        public InfoBarController Health;
        public InfoBarController Manager;

        [Header("User Controller")]
        public UserController User;

        private void Awake()
        {
            Overlay.FlatColor.gameObject.SetActive(true);
            Overlay.ColorCanvas.alpha = 1;
            PlayByPlay.Clear();
        }

        void Start()
        {
            HealthMeters.SetActive(false);
            StartCoroutine(RunGame());
        }

        IEnumerator RunGame ()
        {
            yield return Overlay.HideColor(3);
            yield return RunIntro();

            do
            {
                yield return User.UserTurn();
            }
            while (Health.Percentage > 0 && Manager.Percentage < 100 && Manager.Percentage > 0);
        }

        IEnumerator RunIntro ()
        {
            PlayByPlay.Clear();
            PlayByPlay.DelayFrames = 4;

            yield return new WaitForSeconds(2);
            yield return PlayByPlay.Display(StartQuip1);

            yield return new WaitForSeconds(2);
            yield return PlayByPlay.Display(StartQuip2);

            yield return new WaitForSeconds(1);
            Monster.SetTrigger(StartTrigger);

            yield return new WaitForSeconds(0.3f);
            yield return Overlay.ShowColor(Color.white,0.2f);
            yield return new WaitForSeconds(0.2f);
            GameMusic.PlayMusic();
            HealthMeters.SetActive(true);
            Monster.SetTrigger(ContinueTrigger);
            PlayByPlay.Clear();
            yield return Overlay.HideColor(0.2f);

            yield return new WaitForSeconds(1);
        }
    }
}