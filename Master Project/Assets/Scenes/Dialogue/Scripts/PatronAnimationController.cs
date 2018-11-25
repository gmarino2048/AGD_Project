using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronAnimationController : MonoBehaviour {

    //which monster
    public string monsterType;

    //animator next to patron
    public Animator patronAnimator;

	// Use this for initialization
	void Start () {
        monsterType = "Nessie";
        patronAnimator.SetTrigger("Nessie");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Neutral(){
        patronAnimator.SetTrigger("Neutral");
    }

    void Happy()
    {
        patronAnimator.SetTrigger("Happy");
    }

    void Sad()
    {
        patronAnimator.SetTrigger("Sad");
    }

    void End()
    {
        patronAnimator.SetTrigger("Ending");
    }
}

