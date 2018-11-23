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


        public void PlaySizzle()
        {
            SFXPlayer.PlayOneShot(Sizzle, SFXScaler);
        }

        public void PlaySpatula()
        {

        }
    }
}