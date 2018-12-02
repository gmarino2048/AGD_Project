using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Options
{
	public class VolumeSlider : MonoBehaviour
	{
		public VolumeType volumeType;

		private GameSettings _GameSettings;

		void Start () {
			_GameSettings = GameObject.FindObjectOfType<GameSettings>();

			var slider = gameObject.GetComponent<Scrollbar>();
			slider.onValueChanged.AddListener(OnSliderValueChanged);

			switch (volumeType)
			{
				case VolumeType.Master:
					slider.value = _GameSettings.MasterVolume;
					break;
				case VolumeType.Music:
					slider.value = _GameSettings.MusicVolume;
					break;
				case VolumeType.SFX:
					slider.value = _GameSettings.SfxVolume;
					break;
			}
		}

		private void OnSliderValueChanged(float value)
		{
			switch (volumeType)
			{
				case VolumeType.Master:
					_GameSettings.MasterVolume = value;
					break;
				case VolumeType.Music:
					_GameSettings.MusicVolume = value;
					break;
				case VolumeType.SFX:
					_GameSettings.SfxVolume = value;
					break;
			}
		}
	}
}