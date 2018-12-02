using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class SFXController : MonoBehaviour {

        [Header("Audio Sources")]
        public AudioSource Regular;
        public AudioSource ManagerCritical;
        public AudioSource HealthCritical;

        [Header("Sound Files")]
        public AudioClip ManagerMeterDown;
        public AudioClip ManagerMeterUp;
        public AudioClip ManagerMeterCritical;
        public AudioClip ManagerMeterZero;

        public AudioClip HealthUp;
        public AudioClip HealthDown;
        public AudioClip HealthLow;
        public AudioClip HealthZero;

        public AudioClip MonsterHit;
        public AudioClip PlayerHit;
        public AudioClip MonsterMiss;

        [Header("Reference Objects")]
        public InfoBarController Health;
        public InfoBarController Manager;

        private void Update()
        {
            if (Manager.Percentage > 80 && !ManagerCritical.isPlaying)
            {
                ManagerCritical.clip = ManagerMeterCritical;
                ManagerCritical.loop = true;
                ManagerCritical.Play();
            }
            else if (ManagerCritical.isPlaying)
            {
                ManagerCritical.Stop();
            }

            if (Health.Percentage < 20 && !HealthCritical.isPlaying)
            {
                HealthCritical.clip = ManagerMeterCritical;
                HealthCritical.loop = true;
                HealthCritical.Play();
            }
            else if (HealthCritical.isPlaying)
            {
                HealthCritical.Stop();
            }
        }
    }
}