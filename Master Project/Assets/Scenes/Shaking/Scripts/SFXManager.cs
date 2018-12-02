using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shaking
{
    public class SFXManager : MonoBehaviour
    {

        [Header("Sound Effects")]
        public AudioSource SFX;
        public AudioClip ShakerSound;
        public AudioSource Background;
        public AudioClip BackgroundSound;

        private GameSettings _GameSettings;

        void Awake()
        {
            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings != null)
            {
                _GameSettings.OnChanged += OnGameSettingsChanged;
                OnGameSettingsChanged();
            }
        }


        public void Shake ()
        {
            if (!SFX.isPlaying)
            {
                SFX.PlayOneShot(ShakerSound);
            }

            if (!Background.isPlaying && BackgroundSound != null)
            {
                Background.PlayOneShot(BackgroundSound);
            }
        }

        private void OnGameSettingsChanged()
        {
            SFX.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
            Background.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
        }
    }
}