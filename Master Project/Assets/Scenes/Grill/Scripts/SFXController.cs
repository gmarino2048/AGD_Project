using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grill
{
    public class SFXController : MonoBehaviour
    {

        [Header("Audio Settings")]
        public AudioSource SFXPlayer;
        public float SFXScaler;

        [Header("Sound Effect Clips")]
        public AudioClip Sizzle;
        public AudioClip Spatula;
        public AudioClip Fire;


        public void PlaySizzle()
        {
            SFXPlayer.PlayOneShot(Sizzle, SFXScaler);
        }

        public void PlayFire()
        {
            SFXPlayer.clip = Fire;
            SFXPlayer.loop = true;
            SFXPlayer.Play();
        }
    }
}