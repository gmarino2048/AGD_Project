using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeBehavior : MonoBehaviour {

    [Header("Screen Information")]
    [SerializeField]
    public Camera Camera;
    public float XOffset;
    public float YOffset;
    public float ZIndex;

    [Header("Scene References")]
    [SerializeField]
    public Collider2D ItemToChop;
    public float BufferWidth;

	// Use this for initialization
	void Start () 
    {
        // Set the inital position of the knife
        float initialX = Camera.transform.position.x + XOffset;
        float initialY = Camera.transform.position.y + YOffset;

        Vector3 knifePosition = new Vector3(initialX, initialY, ZIndex);
        gameObject.transform.position = knifePosition;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
