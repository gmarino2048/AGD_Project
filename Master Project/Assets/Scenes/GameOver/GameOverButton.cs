using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour {

    /// <summary>
    /// The name of the next scene.
    /// </summary>
    public string nextSceneName;

    // Update is called once per frame
    void OnMouseDown()
    {
        Debug.Log("grrrr");
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    public void MyButtonClick()
    {
        Debug.Log("grrrr");
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
