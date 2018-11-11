using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NameEntry
{
	public class NameEntrySceneManager : MonoBehaviour
	{
		/// <summary>
		/// The input field that the player puts their name into
		/// </summary>
		public InputField nameInputField;

		/// <summary>
		/// The button that the player clicks to move forward
		/// </summary>
		public Button continueButton;

		/// <summary>
		/// The scene to go to after this one
		/// </summary>
		public string nextSceneName;

		private GameSettings _GameSettings;

		// Use this for initialization
		void Start () {
			_GameSettings = GameObject.FindObjectOfType<GameSettings>();
			continueButton.onClick.AddListener(OnContinueButtonClicked);
		}

		private void OnContinueButtonClicked()
		{
			_GameSettings.PlayerName = nameInputField.text;
			SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
		}
	}
}