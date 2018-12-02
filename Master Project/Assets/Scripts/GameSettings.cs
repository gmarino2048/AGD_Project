using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public delegate void ChangeEventHandler();
	public event ChangeEventHandler OnChanged;

	private string _playerName;
	/// <summary>
	/// The name the player chose to use
	/// </summary>
	public string PlayerName
	{
		get {
			return _playerName;
		}
		set {
			if (OnChanged != null)
				OnChanged();
			_playerName = value;
		}
	}

	private int _framesPerCharacter = 1;
	/// <summary>
	/// How many frames are shown in between each character for text scrolling
	/// </summary>
	public int FramesPerCharacter
	{
		get {
			return _framesPerCharacter;
		}
		set {
			if (OnChanged != null)
				OnChanged();
			_framesPerCharacter = value;
		}
	}

	private float _musicVolume = 1;
	/// <summary>
	/// Volume for music
	/// </summary>
	public float MusicVolume
	{
		get {
			return _musicVolume;
		}
		set {
			if (OnChanged != null)
				OnChanged();
			_musicVolume = value;
		}
	}

	private float _sfxVolume = 1;
	/// <summary>
	/// Volume for SFX
	/// </summary>
	public float SfxVolume
	{
		get {
			return _sfxVolume;
		}
		set {
			if (OnChanged != null)
				OnChanged();
			_sfxVolume = value;
		}
	}

	private float _masterVolume = 1;
	/// <summary>
	/// Volume for master
	/// </summary>
	public float MasterVolume
	{
		get {
			return _masterVolume;
		}
		set {
			if (OnChanged != null)
				OnChanged();
			_masterVolume = value;
		}
	}
}
