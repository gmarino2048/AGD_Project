using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * starts the dialogue for a character
* */
public class StartDialogue : MonoBehaviour {

	/* 
	 * the dialogue for the character
	 * */
	public Dialogue dialogue;

	/* 
	 * starts or triggers the dialogue
	 * */
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
