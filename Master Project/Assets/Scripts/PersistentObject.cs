using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
    private bool isPersistent = false;

    void Awake()
    {
        Debug.Log("Persisting object");
        DontDestroyOnLoad(this.gameObject);
        isPersistent = true;
    }
}