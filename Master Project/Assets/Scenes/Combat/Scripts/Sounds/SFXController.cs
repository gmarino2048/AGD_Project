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
        public float SFXVolume;

        bool Ready;

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

        private void Start()
        {
            Regular.volume = SFXVolume;
            ManagerCritical.volume = SFXVolume;
            HealthCritical.volume = SFXVolume;
        }

        public void Activate() { Ready = true; }

        private void Update()
        {
            if (Ready)
            {
                if (Manager.Percentage > 80 && !ManagerCritical.isPlaying)
                {
                    ManagerCritical.clip = ManagerMeterCritical;
                    ManagerCritical.loop = true;
                    ManagerCritical.Play();
                }
                if (Manager.Percentage <= 80 && ManagerCritical.isPlaying)
                {
                    ManagerCritical.Stop();
                }

                if (Health.Percentage < 20 && !HealthCritical.isPlaying)
                {
                    HealthCritical.clip = ManagerMeterCritical;
                    HealthCritical.loop = true;
                    HealthCritical.Play();
                }
                if (Health.Percentage >= 20 && HealthCritical.isPlaying)
                {
                    HealthCritical.Stop();
                }
            }
        }

        public IEnumerator MonsterIsHit ()
        {
            Regular.PlayOneShot(MonsterHit);
            yield return new WaitWhile(() => Regular.isPlaying);

            Regular.PlayOneShot(ManagerMeterDown);
            yield return null;
        }

        public IEnumerator PlayerIsHit()
        {
            Regular.PlayOneShot(PlayerHit);
            yield return new WaitWhile(() => Regular.isPlaying);

            Regular.PlayOneShot(HealthDown);
            yield return null;
        }

        public IEnumerator MonsterMisses()
        {
            Regular.PlayOneShot(MonsterMiss);
            yield return null;
        }

        public IEnumerator MonsterHeal ()
        {
            Regular.PlayOneShot(ManagerMeterUp);
            yield return null;
        }

        public IEnumerator PlayerHeal ()
        {
            Regular.PlayOneShot(HealthUp);
            yield return null;
        }

        public IEnumerator PlayerDie()
        {
            Regular.PlayOneShot(HealthZero);
            yield return null;
        }

        public IEnumerator MonsterDie()
        {
            Regular.PlayOneShot(ManagerMeterZero);
            yield return null;
        }
    }
}