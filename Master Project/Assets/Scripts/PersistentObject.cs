using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static bool isCreated = false;

    void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(this.gameObject);
            isCreated = true;
        }
    }
}