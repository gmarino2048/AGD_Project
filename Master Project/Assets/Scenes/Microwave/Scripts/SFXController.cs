using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microwave
{
    public class SFXController : MonoBehaviour
    {

        [Header("Audio Controls")]
        public AudioSource SFXPlayer;
        public float SFXScaler;

        [Header("Audio Clips")]
        public AudioClip MicrowaveStart;
        public AudioClip MicrowaveOpen;
        public AudioClip MicrowaveBeep;

        public AudioClip MicrowaveExtra;

        public void PlayStart () 
        {
            SFXPlayer.PlayOneShot(MicrowaveStart, SFXScaler);
        }

        public void PlayOpen () 
        {
            SFXPlayer.PlayOneShot(MicrowaveOpen, SFXScaler);
        }

        public void PlayExtra () 
        {
            SFXPlayer.clip = MicrowaveExtra;
            SFXPlayer.loop = true;

            SFXPlayer.Play();
        }

        public void PlayBeep ()
        {
            SFXPlayer.clip = MicrowaveBeep;
            SFXPlayer.loop = true;

            SFXPlayer.Play();
        }
    }
}