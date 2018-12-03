using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ingredients
{
    public class SFXController : MonoBehaviour
	{

    	[Header("Sound Effects")]
    	public AudioSource Source;
    	public AudioClip CorrectIngredient;
    	public AudioClip IncorrectIngredient;

        private GameSettings _GameSettings;

        void Awake()
        {
            _GameSettings = GameObject.FindObjectOfType<GameSettings>();
            if (_GameSettings != null)
            {
                _GameSettings.OnChanged += OnGameSettingsChanged;
                OnGameSettingsChanged();
				SceneManager.sceneUnloaded += (Scene scene) => _GameSettings.OnChanged -= OnGameSettingsChanged;
            }
        }

    	public void playForCorrectIngredient()
		{
    		Source.PlayOneShot (CorrectIngredient);
    	}

    	public void playForIncorrectIngredient()
		{
    		Source.PlayOneShot (IncorrectIngredient);
    	}

		private void OnGameSettingsChanged()
		{
			Source.volume = _GameSettings.SfxVolume * _GameSettings.MasterVolume;
		}
    }
}
