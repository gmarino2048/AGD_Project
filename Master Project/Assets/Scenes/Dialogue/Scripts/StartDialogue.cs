using System;
using UnityEngine;

/// <summary>
/// Starts a dialogue
/// </summary>
public class StartDialogue : MonoBehaviour {

	/// <summary>
    /// The ID of the monster to start a dialogue with
    /// </summary>
	public Guid MonsterID;
    
    /// <summary>
    /// Starts the dialogue
    /// </summary>
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(MonsterID);
	}
}
