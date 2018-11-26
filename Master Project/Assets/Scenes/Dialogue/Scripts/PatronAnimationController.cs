using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using System;
using UnityEngine.UI;


public class PatronAnimationController : MonoBehaviour {

    //which monster
    public Guid monsterID;

    //animator next to patron
    public Animator patronAnimator;

    //info on which monster
    private MonsterData _MonsterData;

    // Use this for initialization
    void Start () {
        var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
        monsterID = gameNarrativeManager.CurrentStage.MonsterID;

        if(true /*monsterID == nessie*/){
            patronAnimator.SetTrigger("Nessie");
            patronAnimator.SetTrigger("Neutral");
        }

        if (false /*monsterID == cerberus*/)
        {
            patronAnimator.SetTrigger("cerberus");
            patronAnimator.SetTrigger("Neutral");
        }

        if (false /*monsterID == redacted*/)
        {
            patronAnimator.SetTrigger("redacted");
            patronAnimator.SetTrigger("Neutral");
        }



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Neutral(){
        patronAnimator.SetTrigger("Neutral");
    }

    public void Happy()
    {
        patronAnimator.SetTrigger("Happy");
    }

    public void Sad()
    {
        patronAnimator.SetTrigger("Sad");
    }

    public void End()
    {
        patronAnimator.SetTrigger("Neutral");
        patronAnimator.SetTrigger("Ending");
    }
}

