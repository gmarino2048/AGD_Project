using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	public AudioSource dialogueAndMonologueAudio;
	public AudioSource cookingAudio;

	private HashSet<string> _DIALOGUE_AND_MONOLOGUE_SCENE_NAMES = new HashSet<string> {
        "Monologue", "DialogueScene", "NameEntry", "FoodScoreScene", "Main Menu"
    };
	private HashSet<string> _COOKING_SCENE_NAMES = new HashSet<string> {
		"Chopping", "Grill", "IngredientsScene", "Microwaving", "Shaking", "Stirring"
	};
	private AudioSource _CurrentAudioSource = null;
	private GameSettings _GameSettings;

	void Awake () {
		_GameSettings = GameObject.FindObjectOfType<GameSettings>();
		_GameSettings.OnChanged += OnGameSettingsChanged;
		
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (_DIALOGUE_AND_MONOLOGUE_SCENE_NAMES.Contains(scene.name)) {
			StartAudio(dialogueAndMonologueAudio);
		}
		else if (_COOKING_SCENE_NAMES.Contains(scene.name)) {
			StartAudio(cookingAudio);
		}
		else if (_CurrentAudioSource != null) {
			_CurrentAudioSource.Stop();
			_CurrentAudioSource = null;
		}
	}

	private void OnGameSettingsChanged()
	{
		if (_CurrentAudioSource == null) {
			return;
		}
		_CurrentAudioSource.volume = _GameSettings.MusicVolume * _GameSettings.MasterVolume;
	}

	private void StartAudio(AudioSource audioSource) {
		if (audioSource == _CurrentAudioSource) {
			return;
		}
	
		if (_CurrentAudioSource != null) {
			_CurrentAudioSource.Stop();
		}

		_CurrentAudioSource = audioSource;
		_CurrentAudioSource.volume = _GameSettings.MusicVolume * _GameSettings.MasterVolume;
		_CurrentAudioSource.Play();
	}
}
