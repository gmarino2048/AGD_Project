using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerHandler : MonoBehaviour {

    public GameObject Info;
    // Use this for initialization
    private void Awake()
    {
        Info.SetActive(false);
    }
	// Update is called once per frame
}
