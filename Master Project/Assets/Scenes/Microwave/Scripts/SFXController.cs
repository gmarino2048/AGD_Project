using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Microwave
{
    public class SFXController : MonoBehaviour
    {

        [Header("Audio Controls")]
        public AudioSource MicrowaveBackground;
        public AudioSource SFXPlayer;

        [Header("Audio Clips")]
        public AudioClip MicrowaveStart;
        public AudioClip MicrowaveOpen;
        public AudioClip MicrowaveBeep;

        public AudioClip MicrowaveExtra;

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

        public void PlayStart () 
        {
            MicrowaveBackground.PlayOneShot(MicrowaveStart);
        }

        public void PlayOpen () 
        {
            MicrowaveBackground.Stop();
            SFXPlayer.Stop();
            SFXPlayer.PlayOneShot(MicrowaveOpen);
        }

        public void PlayExtra () 
        {
            SFXPlayer.clip = MicrowaveExtra;
            SFXPlayer.loop = true;

            SFXPlayer.Play();
        }

        public void PlayBeep ()
        {
            MicrowaveBackground.Stop();
            SFXPlayer.Stop();

            SFXPlayer.clip = MicrowaveBeep;
            SFXPlayer.loop = true;

            SFXPlayer.Play();
        }

        private void OnGameSettingsChanged()
        {
            MicrowaveBackground.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
            SFXPlayer.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
        }
    }
}