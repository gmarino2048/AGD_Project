using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        [Header("Overlay Manager")]
        public OverlayController Overlay;

        [Header("Music Controller")]
        public GameController Controller;
        public MusicController GameMusic;
        public SFXController SFX;

        [Header("Health Bars")]
        public GameObject HealthMeters;
        public InfoBarController Health;
        public InfoBarController Manager;

        [Header("Combatant Controllers")]
        public UserController User;
        public MonsterController Monster;

        private readonly string _GAME_OVER_SCENE_NAME = "Game OVer";
        private readonly string _DIALOGUE_SCENE_NAME = "DialogueScene";
        private readonly string _MONOLOGUE_SCENE_NAME = "Monologue";

        public enum GameEndConditions
        {
            Win,
            Lose,
            Die
        }

        public GameEndConditions EndCondition { get; private set; }

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
            yield return SFX.EnterMonster();
            yield return Overlay.HideColor(1f);
            yield return RunIntro();
            SFX.Activate();

            do
            {
                yield return User.UserTurn();

                if (!(Health.Percentage > 0 && Manager.Percentage < 100 && Manager.Percentage > 0)) break;

                yield return Monster.MonsterTurn();
                Controller.HealMonster(5);
            }
            while (Health.Percentage > 0 && Manager.Percentage < 100 && Manager.Percentage > 0);

            if (Health.Percentage <= 0) EndCondition = GameEndConditions.Die;
            if (Manager.Percentage >= 100) EndCondition = GameEndConditions.Lose;
            if (Manager.Percentage <= 0) EndCondition = GameEndConditions.Win;

            yield return RunOutro();
        }

        IEnumerator RunIntro ()
        {
            PlayByPlay.Clear();
            PlayByPlay.DelayFrames = 1;

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
            PlayByPlay.DelayFrames = 1;

            yield return new WaitForSeconds(1);

            GameMusic.StopMusic();
            SFX.StopMusic();
            switch (EndCondition)
            {
                case GameEndConditions.Win:
                    yield return SFX.Win();
                    break;
                case GameEndConditions.Lose:
                    yield return SFX.Lose();
                    break;
                case GameEndConditions.Die:
                    yield return SFX.Death();
                    break;
            }

            yield return PlayByPlay.DisplayOutro(EndCondition);
            yield return Overlay.ShowColor(new Color(57, 45, 77), 3f);

            if (EndCondition == GameEndConditions.Win)
            {
                yield return new WaitForSeconds(1f);
                var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
                if (gameNarrativeManager == null)
                {
                    SceneManager.LoadScene(_DIALOGUE_SCENE_NAME);
                }

                if (gameNarrativeManager.AnyStagesLeft())
                {
                    SceneManager.LoadScene(_DIALOGUE_SCENE_NAME, LoadSceneMode.Single);
                }
                else
                {
                    SceneManager.LoadScene(_MONOLOGUE_SCENE_NAME, LoadSceneMode.Single);
                }
            }
            else
            {
                SceneManager.LoadScene(_GAME_OVER_SCENE_NAME, LoadSceneMode.Single);
            }
        }
    }
}