using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopObjectBehavior : MonoBehaviour {

    public BoxCollider2D ChopObject { get; private set; }

    public float LeftBound { get; private set; }
    public float RightBound { get; private set; }

    void Awake()
    {
        ChopObject = gameObject.GetComponentInChildren<BoxCollider2D>();
        SetBounds(ChopObject);
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
