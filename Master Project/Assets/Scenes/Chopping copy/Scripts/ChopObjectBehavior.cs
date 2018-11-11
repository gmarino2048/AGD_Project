using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chopping
{
    public class ChopObjectBehavior : MonoBehaviour
    {

        #region Parameters

        public BoxCollider2D ChopObject { get; private set; } // The bounds of this object's child sprite.

        public float LeftBound { get; private set; } // The left bound of the child sprite's collider.
        public float RightBound { get; private set; } // The right bound of the child sprite's collider.

        #endregion

        #region MonoBehaviour

        /// <summary>
        /// Called before the game even starts. Will run before Start() and set
        /// the boundaries for this object.
        /// </summary>
        void Awake()
        {
            ChopObject = gameObject.GetComponentInChildren<BoxCollider2D>();
            SetBounds(ChopObject);
        }

        #endregion

        #region Auxiliary

        /// <summary>
        /// Sets the LeftBound and RightBound of this object based on the child
        /// sprite's box collider.
        /// </summary>
        /// <param name="boundaries">The box collider of the child sprite.</param>
        void SetBounds(BoxCollider2D boundaries)
        {
            // Get global position of the collider
            Vector3 position = boundaries.gameObject.transform.position;

            // Set Horizontal bounds based on extents of collider
            float halfWidth = boundaries.bounds.extents.x;

            LeftBound = position.x - halfWidth;
            RightBound = position.x + halfWidth;
        }

        #endregion
    }
}