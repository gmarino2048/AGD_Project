using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chopping
{
    public class SFXController : MonoBehaviour
    {

        [Header("Audio Source")]
        public AudioSource SFXPlayer;
        public float SFXScale;

        [Header("Audio Clips")]
        public AudioClip KnifeCut;

        private GameSettings _GameSettings;

        void Awake()
        {
            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings != null)
            {
                _GameSettings.OnChanged += OnGameSettingsChanged;
                OnGameSettingsChanged();
				SceneManager.sceneUnloaded += (Scene scene) => _GameSettings.OnChanged -= OnGameSettingsChanged;
            }
        }

        public void PlayCut()
        {
            SFXPlayer.PlayOneShot(KnifeCut, SFXScale);
        }

        private void OnGameSettingsChanged()
        {
            SFXPlayer.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
        }
    }
}