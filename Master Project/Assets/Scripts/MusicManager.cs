using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	public AudioSource dialogueAndMonologueAudio;
	public AudioSource cookingAudio;

	private HashSet<string> _DIALOGUE_AND_MONOLOGUE_SCENE_NAMES = new HashSet<string> {
		"Monologue", "DialogueScene"
	};
	private HashSet<string> _COOKING_SCENE_NAMES = new HashSet<string> {
		"Chopping", "Grill", "IngredientsScene", "Microwaving", "Shaking", "Stirring"
	};
	private AudioSource _CurrentAudioSource = null;

	void Awake () {
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

	private void StartAudio(AudioSource audioSource) {
		if (audioSource == _CurrentAudioSource) {
			return;
		}
	
		if (_CurrentAudioSource != null) {
			_CurrentAudioSource.Stop();
		}

		_CurrentAudioSource = audioSource;
		_CurrentAudioSource.Play();
	}
}
