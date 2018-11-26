using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stirring
{
    public class SFXManager : MonoBehaviour
    {

        [Header("SFX Settings")]
        public AudioSource SFXPlayer;
        public float SFXScaler;

        [Header("Audio Clip")]
        public AudioClip StirringAudio;

        public void PlayClip() 
        {
            SFXPlayer.clip = StirringAudio;
            SFXPlayer.volume = SFXScaler;
            SFXPlayer.loop = true;

            SFXPlayer.Play();
        }

        public void StopClip() { SFXPlayer.Stop(); }
    }
}