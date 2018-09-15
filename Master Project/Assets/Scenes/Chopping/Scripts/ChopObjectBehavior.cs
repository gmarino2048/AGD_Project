using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopObjectBehavior : MonoBehaviour {

    [Header("Sprite Information")]
    [SerializeField]
    public BoxCollider2D ChopObject;

    public float LeftBound;
    public float RightBound;

	// Use this for initialization
	void Start () {
        ChopObject = gameObject.GetComponentInChildren<BoxCollider2D>();

        SetBounds(ChopObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetBounds (BoxCollider2D boundaries) {
        // Get global position of the collider
        Vector3 position = boundaries.gameObject.transform.position;

        // Set Horizontal bounds based on extents of collider
        float halfWidth = boundaries.bounds.extents.x;

        LeftBound = position.x - halfWidth;
        RightBound = position.x + halfWidth;
    }
}
