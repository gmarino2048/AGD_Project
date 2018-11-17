using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Monsters;

public class AudioController : MonoBehaviour {

    private ManagerBar Mb;
    private HealthBar Hb;
    private MonsterNames Names = new MonsterNames();
    private CombatInitiator _CombatInitiator;
    public AudioSource Source;
    public AudioSource LoopSource;
    public AudioSource LoopSource1;
    public AudioSource Nessie_BGM;
    public AudioSource Cerberus_BGM;
    public AudioSource REDACTED_BGM;
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
    public MonsterData CurrentMonster;
    // Use this for initialization
    void Awake () {
        //Source = GetComponent<AudioSource>();
        //LoopSource = GetComponent<AudioSource>();
        Mb = GameObject.Find("ManagerBar").GetComponent<ManagerBar>();
        Hb = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }
	
    void Start()
    {
        _CombatInitiator = GameObject.FindObjectOfType<CombatInitiator>();
        if (_CombatInitiator != null)
        {
            var monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            CurrentMonster = monsterFactory.LoadMonster(_CombatInitiator.MonsterID);
            if (CurrentMonster.ToString() == Names._NESSIE_NAME)
            {
                Nessie_BGM.Play();
                Cerberus_BGM.Stop();
                REDACTED_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._CERBERUS_NAME)
            {
                Nessie_BGM.Stop();
                Cerberus_BGM.Play();
                REDACTED_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._REDACTED_NAME)
            {
                Nessie_BGM.Stop();
                Cerberus_BGM.Stop();
                REDACTED_BGM.Play();
            }
        }
        else
        {
            CurrentMonster = new MonsterData("Cerberus", 0, null, null);
            if (CurrentMonster.ToString() == Names._NESSIE_NAME)
            {
                Nessie_BGM.Play();
                Cerberus_BGM.Stop();
                REDACTED_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._CERBERUS_NAME)
            {
                Nessie_BGM.Stop();
                Cerberus_BGM.Play();
                REDACTED_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._REDACTED_NAME)
            {
                Nessie_BGM.Stop();
                Cerberus_BGM.Stop();
                REDACTED_BGM.Play();
            }
        }
    }
	// Update is called once per frame
	void Update () {
		if(Mb.GetCurrentBarValue() == 100 || Mb.GetCurrentBarValue() == 0)
        {
            LoopSource.Stop();
            LoopSource1.Stop();
            if (CurrentMonster.ToString() == Names._NESSIE_NAME)
            {
                Nessie_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._CERBERUS_NAME)
            {
                Cerberus_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._REDACTED_NAME)
            {
                REDACTED_BGM.Stop();
            }
        }
        if (Hb.GetCurrentHealthValue() == 0)
        {
            LoopSource.Stop();
            LoopSource1.Stop();
            if (CurrentMonster.ToString() == Names._NESSIE_NAME)
            {
                Nessie_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._CERBERUS_NAME)
            {
                Cerberus_BGM.Stop();
            }
            else if (CurrentMonster.ToString() == Names._REDACTED_NAME)
            {
                REDACTED_BGM.Stop();
            }
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
