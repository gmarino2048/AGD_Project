using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class MusicController : MonoBehaviour
    {

        [Header("Components")]
        public AudioSource MusicPlayer;
        public AudioClip Music;

        private GameSettings _GameSettings;

        void Awake()
        {
            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings != null)
            {
                _GameSettings.OnChanged += OnGameSettingsChanged;
                OnGameSettingsChanged();
            }

            List<AudioSource> sources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
            sources.ForEach((source) => source.Stop());
        }

        public void PlayMusic ()
        {
            MusicPlayer.clip = Music;
            MusicPlayer.loop = true;

            MusicPlayer.Play();
        }

        public void StopMusic ()
        {
            MusicPlayer.Stop();
        }

        public void AdjustVolume(float newVolume)
        {
            MusicPlayer.volume = newVolume;
        }
        
        private void OnGameSettingsChanged()
        {
            MusicPlayer.volume = _GameSettings.MusicVolume * _GameSettings.MasterVolume;
        }
    }
}