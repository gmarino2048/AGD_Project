using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stirring
{
    public class SFXManager : MonoBehaviour
    {

        [Header("SFX Settings")]
        public AudioSource SFXPlayer;
        public float SFXScaler;

        [Header("Audio Clip")]
        public AudioClip StirringAudio;

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

        public void PlayClip() 
        {
            SFXPlayer.clip = StirringAudio;
            SFXPlayer.volume = SFXScaler;
            SFXPlayer.loop = true;

            SFXPlayer.Play();
        }

        public void StopClip() { SFXPlayer.Stop(); }

        private void OnGameSettingsChanged()
        {
            SFXPlayer.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
        }
    }
}