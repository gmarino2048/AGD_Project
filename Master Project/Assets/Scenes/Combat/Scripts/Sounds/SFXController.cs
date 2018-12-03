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
        public AudioClip MonsterEnter;

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

        public AudioClip GameWin;
        public AudioClip GameLose;
        public AudioClip Die;

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

        public void StopMusic ()
        {
            Regular.Stop();
            HealthCritical.Stop();
            ManagerCritical.Stop();
        }

        public IEnumerator EnterMonster()
        {
            Regular.PlayOneShot(MonsterEnter, SFXVolume);
            yield return null;
        }

        public IEnumerator Win ()
        {
            Regular.PlayOneShot(GameWin, SFXVolume);
            yield return null;
        }

        public IEnumerator Lose()
        {
            Regular.PlayOneShot(GameLose, SFXVolume);
            yield return null;
        }

        public IEnumerator Death()
        {
            Regular.PlayOneShot(Die, SFXVolume);
            yield return null;
        }

        public IEnumerator MonsterIsHit ()
        {
            Regular.PlayOneShot(MonsterHit, SFXVolume);
            yield return new WaitWhile(() => Regular.isPlaying);

            Regular.PlayOneShot(ManagerMeterDown, SFXVolume);
            yield return null;
        }

        public IEnumerator PlayerIsHit()
        {
            Regular.PlayOneShot(PlayerHit);
            yield return new WaitWhile(() => Regular.isPlaying);

            Regular.PlayOneShot(HealthDown, SFXVolume);
            yield return null;
        }

        public IEnumerator MonsterMisses()
        {
            Regular.PlayOneShot(MonsterMiss, SFXVolume);
            yield return null;
        }

        public IEnumerator MonsterHeal ()
        {
            Regular.PlayOneShot(ManagerMeterUp, SFXVolume);
            yield return null;
        }

        public IEnumerator PlayerHeal ()
        {
            Regular.PlayOneShot(HealthUp, SFXVolume);
            yield return null;
        }

        public IEnumerator PlayerDie()
        {
            Regular.PlayOneShot(HealthZero, SFXVolume);
            yield return null;
        }

        public IEnumerator MonsterDie()
        {
            Regular.PlayOneShot(ManagerMeterZero, SFXVolume);
            yield return null;
        }
    }
}