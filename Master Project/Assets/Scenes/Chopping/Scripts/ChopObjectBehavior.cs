using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopObjectBehavior : MonoBehaviour {

    /// <summary>
    /// The Box Collider surrounding the sprite of the choppable object.
    /// </summary>
    /// <value>The box collider attached to this object's sprite.</value>
    public BoxCollider2D ChopObject { get; private set; }

    /// <summary>
    /// The left hand side of the sprite's box collider.
    /// </summary>
    /// <value>A float value signifying the left bound of the
    /// collider.</value>
    public float LeftBound { get; private set; }

    /// <summary>
    /// The right hand side of the sprite's box collider.
    /// </summary>
    /// <value>The right bound.</value>
    public float RightBound { get; private set; }


    /// <summary>
    /// Called before the game even starts. Will run before Start() and set
    /// the boundaries for this object.
    /// </summary>
    void Awake()
    {
        ChopObject = gameObject.GetComponentInChildren<BoxCollider2D>();
        SetBounds(ChopObject);
    }


    /// <summary>
    /// Sets the LeftBound and RightBound of this object based on the child
    /// sprite's box collider.
    /// </summary>
    /// <param name="boundaries">The box collider of the child sprite.</param>
    void SetBounds (BoxCollider2D boundaries) {
        // Get global position of the collider
        Vector3 position = boundaries.gameObject.transform.position;

        // Set Horizontal bounds based on extents of collider
        float halfWidth = boundaries.bounds.extents.x;

        LeftBound = position.x - halfWidth;
        RightBound = position.x + halfWidth;
    }
}
