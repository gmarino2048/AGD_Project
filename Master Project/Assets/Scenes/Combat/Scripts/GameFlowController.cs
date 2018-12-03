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
        public Animator MonsterAnimator;
        public string StartTrigger = "start";
        public string ContinueTrigger = "continue";

        [Header("Outro Phrases")]
        public List<string> EndPhrases;

        [Header("Overlay Manager")]
        public OverlayController Overlay;

        [Header("Music Controller")]
        public MusicController GameMusic;
        public SFXController SFX;

        [Header("Health Bars")]
        public GameObject HealthMeters;
        public InfoBarController Health;
        public InfoBarController Manager;

        [Header("Combatant Controllers")]
        public UserController User;
        public MonsterController Monster;

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
            SFX.Activate();

            do
            {
                yield return User.UserTurn();

                if (!(Health.Percentage > 0 && Manager.Percentage < 100 && Manager.Percentage > 0)) break;

                yield return Monster.MonsterTurn();
            }
            while (Health.Percentage > 0 && Manager.Percentage < 100 && Manager.Percentage > 0);

            RunOutro();
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
            MonsterAnimator.SetTrigger(StartTrigger);

            yield return new WaitForSeconds(0.3f);
            yield return Overlay.ShowColor(Color.white,0.2f);
            yield return new WaitForSeconds(0.2f);
            GameMusic.PlayMusic();
            HealthMeters.SetActive(true);
            MonsterAnimator.SetTrigger(ContinueTrigger);
            PlayByPlay.Clear();
            yield return Overlay.HideColor(0.2f);

            yield return new WaitForSeconds(1);
        }

        IEnumerator RunOutro ()
        {
            PlayByPlay.Clear();
            PlayByPlay.DelayFrames = 4;

            foreach (var endPhrase in EndPhrases) {
                yield return PlayByPlay.Display(endPhrase);
                yield return new WaitForSeconds(2);
            }
            
            //TODO
        }
    }
}