using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
* the DialogueManager class displays the dialogue through text scrolling
* */
public class DialogueManager : MonoBehaviour {

	/* 
	 * the text for the name of the character for the dialogue
	 * */
	public Text nameText;

	/* 
	 * the text for the name of the character for the dialogue
	 * */
	public Text dialogueText;

	/* 
	 * the list of sentences for the dialogue
	 * */
	private Queue<string> sentences;

	/* 
	 * used for initialization
	 * */
	void Start () {
		sentences = new Queue<string>();
	}


	/* 
	 * starts and displays the dialogue for each sentence in the list of sentences
	 * */
	public void StartDialogue (Dialogue dialogue)
	{
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	/* 
	 * the sentence is displayed using a coroutine
	 * */
	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			//EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	/* 
	 * the sentence is typed character by character
	 * */
	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}
}
