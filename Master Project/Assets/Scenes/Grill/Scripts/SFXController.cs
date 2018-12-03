using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grill
{
    public class SFXController : MonoBehaviour
    {

        [Header("Audio Settings")]
        public AudioSource SFXPlayer;
        float Volume;

        [Header("Sound Effect Clips")]
        public AudioClip Meat;
        public AudioClip Sizzle;
        public AudioClip Spatula;
        public AudioClip Fire;

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


        public void PlayMeat ()
        {
            SFXPlayer.PlayOneShot(Meat, Volume);
        }

        public void PlaySizzle()
        {
            SFXPlayer.PlayOneShot(Sizzle, Volume);
        }

        public void PlayFire()
        {
            SFXPlayer.clip = Fire;
            SFXPlayer.loop = true;
            SFXPlayer.volume = Volume;
            SFXPlayer.Play();
        }

        private void OnGameSettingsChanged()
        {
            Volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
        }
    }
}