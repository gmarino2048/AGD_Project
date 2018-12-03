using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients {
    public class SFXController : MonoBehaviour {

    	[Header("Sound Effects")]
    	public AudioSource Source;
    	public AudioClip CorrectIngredient;
    	public AudioClip IncorrectIngredient;

    	public float SFXScaler = 1;

    	public void playForCorrectIngredient() {
    		Source.PlayOneShot (CorrectIngredient);
    	}

    	public void playForIncorrectIngredient() {
    		Source.PlayOneShot (IncorrectIngredient);
    	}
    }
}
