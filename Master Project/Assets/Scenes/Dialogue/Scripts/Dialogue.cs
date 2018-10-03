using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * the dialogue class contains the name of the character and the dialogue
 * */
[System.Serializable]
public class Dialogue {

	/* 
	 * the name of the character for the dialogue
	 * */
	public string name;

	/* 
	 * the dialogue for the character
	 * */
	[TextArea(3, 10)]
	public string[] sentences;

}
