using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class MusicController : MonoBehaviour
    {

        [Header("Components")]
        public float Volume;
        public AudioSource MusicPlayer;
        public AudioClip Music;

        void Awake()
        {
            List<AudioSource> sources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
            sources.ForEach((source) => source.Stop());
        }

        public void PlayMusic ()
        {
            MusicPlayer.clip = Music;
            MusicPlayer.loop = true;
            MusicPlayer.volume = Volume;

            MusicPlayer.Play();
        }

        public void AdjustVolume(float newVolume)
        {
            MusicPlayer.volume = newVolume;
        }
    }
}