using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PauseMenu
{
	public class DialogueDisplay : MonoBehaviour
	{
		public Text dialogDisplay;

		void Awake()
		{
			var gameNarrativeManager = GameObject.FindObjectOfType<GameNarrativeManager>();
			if (gameNarrativeManager == null || gameNarrativeManager.CurrentStage == null)
			{
				return;
			}

			foreach (var dialogueLine in gameNarrativeManager.CurrentStage.DialogueHistory)
			{
				dialogDisplay.text += "\n" + dialogueLine;
			}
		}
	}
}