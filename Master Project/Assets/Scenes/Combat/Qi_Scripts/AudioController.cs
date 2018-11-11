using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class AudioController : MonoBehaviour {

    private ManagerBar Mb;
    private HealthBar Hb;
    public AudioSource Source;
    public AudioSource LoopSource;
    public AudioSource LoopSource1;
    public AudioSource NessieBGM;
    public AudioClip ManagerMeterCritical;
    //public AudioClip ManagerMeterCriticalLowerPitch;
    public AudioClip ManagerMeterGoesDown;
    public AudioClip ManagerMeterGoesUp;
    public AudioClip ManagerMeterHitsZero;
    public AudioClip MonsterGameOver;
    public AudioClip MonsterIsHit;
    public AudioClip PlayerHit;
    public AudioClip PlayerHPGoesDown;
    public AudioClip PlayerHPGoesUp;
    public AudioClip PlayerHPHitsZero;
    public AudioClip PlayerHPLow;
    // Use this for initialization
    void Awake () {
        //Source = GetComponent<AudioSource>();
        //LoopSource = GetComponent<AudioSource>();
        Mb = GameObject.Find("ManagerBar").GetComponent<ManagerBar>();
        Hb = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Mb.GetCurrentBarValue() == 100 || Mb.GetCurrentBarValue() == 0)
        {
            LoopSource.Stop();
            LoopSource1.Stop();
            NessieBGM.Stop();
        }
        if (Hb.GetCurrentHealthValue() == 0)
        {
            LoopSource.Stop();
            LoopSource1.Stop();
            NessieBGM.Stop();
        }
    }

    public void MMC()
    {
        if (Mb.GetCurrentBarValue() < 70)
        {
            LoopSource.Stop();
        }
        else if (!LoopSource.isPlaying)
        {
            LoopSource.Play();
        }
    }

    public void MMGD()
    {
        Source.PlayOneShot(ManagerMeterGoesDown);
    }

    public void MMGU()
    {
        Source.PlayOneShot(ManagerMeterGoesUp);
    }

    public void MMHZ()
    {
        Source.PlayOneShot(ManagerMeterHitsZero);
    }

    public void MGO()
    {
        if (!Source.isPlaying)
        {
            Source.PlayOneShot(MonsterGameOver);
        }
    }

    public void MH()
    {
        Source.PlayOneShot(MonsterIsHit);
    }

    public void PH()
    {
        Source.PlayOneShot(PlayerHit);
    }

    public void PHPGD()
    {
        Source.PlayOneShot(PlayerHPGoesDown);
    }

    public void PHPGU()
    {
        Source.PlayOneShot(PlayerHPGoesUp);
    }

    public void PHPHZ()
    {
        Source.PlayOneShot(PlayerHPHitsZero);
    }
    
    public void PlayerHPL()
    {
        if (Hb.GetCurrentHealthValue() > 30)
        {
            LoopSource1.Stop();
        }
        else if (!LoopSource1.isPlaying)
        {
            LoopSource1.Play();
        }
    }
}
