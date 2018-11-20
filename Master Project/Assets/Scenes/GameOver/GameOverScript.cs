using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    /// <summary>
    /// The name of the next scene.
    /// </summary>
    public string nextSceneName;

	
	// Update is called once per frame
	void OnMouseDown () {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
