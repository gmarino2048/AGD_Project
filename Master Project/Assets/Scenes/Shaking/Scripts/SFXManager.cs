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

        public float SFXScaler = 1;


        public void Shake ()
        {
            if (!SFX.isPlaying)
            {
                SFX.PlayOneShot(ShakerSound, SFXScaler);
            }

            if (!Background.isPlaying && BackgroundSound != null)
            {
                Background.PlayOneShot(BackgroundSound, SFXScaler);
            }
        }
    }
}