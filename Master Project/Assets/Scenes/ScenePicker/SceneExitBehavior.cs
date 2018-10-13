using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExitBehavior : MonoBehaviour {

    public KeyCode Exit = KeyCode.Escape;

    public string SceneName = "ScenePicker";

	/// <summary>
    /// Sets this game object to persist through multiple
    /// scenes.
    /// </summary>
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	/// <summary>
    /// Checks for the escape key and loads the picker scene.
    /// </summary>
	void Update () {
		if (Input.GetKeyDown(Exit))
        {
            SceneManager.LoadScene(SceneName);
        }
	}
}
