using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shaking
{
    public class ShakerBehavior : MonoBehaviour
    {

        public int Shakes { get; private set; } // The number of shakes completed by this object.
        public BoxCollider2D Collider { get; private set; }

        [Header("Timer Object")]
        public TimerBehavior Timer; // The timer object for the scene.

        [Header("Bounding Zones")]
        [SerializeField]
        public BoxCollider2D TopBound; // The top bound that the shaker must enter.
        [SerializeField]
        public BoxCollider2D BottomBound; // The lower bound that the shaker must enter.

        [SerializeField]
        public BoxCollider2D ExclusionZone; // The zone that you can't enter.
        private float ExclusionTop; // The top of the exclusion zone

        // Screen Bounds
        private float ScreenTop; // The top bound of the screen
        private float ScreenLeft; // The left bound of the screen
        private float ScreenRight; // The right bound of the screen

        /// <summary>
        /// This enumerator stores the possible positions for the shaker so
        /// that we can possibly add states later.
        /// </summary>
        private enum Position
        {
            Top,
            Bottom
        }

        private Position LastPosition; // The previous position of the shaker.
        private Vector3 Offset; // The offset from the mouse to the shaker. Used for calculating shaker position.

        /// <summary>
        /// Sets the static values in the scene like the collider, starting position
        /// of the shaker, and making sure that other values in the scene are not null.
        /// </summary>
        private void Start()
        {
            Collider = GetComponent<BoxCollider2D>();
            LastPosition = Position.Top;
            Shakes = 0;
            Offset = new Vector2(0, 0);
            GetBounds();
        }

        /// <summary>
        /// Checks the position of the shaker and increments the counter when the
        /// user has made one complete shake. 
        /// </summary>
        private void Update()
        {
            if (Collider.IsTouching(TopBound) && LastPosition == Position.Bottom)
            {
                LastPosition = Position.Top;
                Debug.Log("Top Entered");

                Shakes++;
            }
            if (Collider.IsTouching(BottomBound) && LastPosition == Position.Top)
            {
                LastPosition = Position.Bottom;
                Debug.Log("Bottom Entered");
            }
        }


        /// <summary>
        /// Gets the bounds made by the exclusion zone and the different parts
        /// of the screen, so that the shaker cannot move outside those bounds.
        /// Sets the global methods in the class so it returns void.
        /// </summary>
        private void GetBounds () {
            // Get exclusion zone bounds
            float exclusionHeight = ExclusionZone.bounds.center.y +
                                          ExclusionZone.bounds.extents.y;
            ExclusionTop = exclusionHeight + Collider.bounds.extents.y;

            // Get the bounds made by the camera
            if (Camera.main.orthographic){
                float height = Camera.main.orthographicSize;
                float width = height * Screen.width / Screen.height;

                float x = Camera.main.transform.position.x;
                float y = Camera.main.transform.position.y;

                ScreenTop = y + height - Collider.bounds.extents.y;
                ScreenLeft = x - width + Collider.bounds.extents.x;
                ScreenRight = x + width - Collider.bounds.extents.x;
            }
        }


        /// <summary>
        /// Gets the offset of the mouse and the center of the shaker.
        /// </summary>
        private void OnMouseDown()
        {
            Offset = transform.position - MouseToWorldPoint();
            Debug.Log(Offset);
        }

        /// <summary>
        /// Updates the mouse position, specifically making sure that the shaker
        /// stays within the bounds of the scene. This script also makes sure that
        /// the shaker can escape the exclusion zone, and that the mouse offset
        /// is constantly updated.
        /// </summary>
        private void OnMouseDrag()
        {
            // Only move if the timer is not done yet
            if (!Timer.Finished)
            {
                Vector3 relativePosition = MouseToWorldPoint() + Offset;
                relativePosition.z = 0;

                if (!Collider.IsTouching(ExclusionZone))
                {
                    // Make sure the shaker stays within the bounds
                    float newX = Mathf.Clamp(relativePosition.x, ScreenLeft, ScreenRight);
                    float newY = Mathf.Clamp(relativePosition.y, ExclusionTop, ScreenTop);
                    Vector3 newPosition = new Vector3(newX, newY, relativePosition.z);

                    transform.position = newPosition;
                }
                else
                {
                    float relativeX = Mathf.Clamp(relativePosition.x, ScreenLeft, ScreenRight);

                    // Make sure that the shaker can escape the exclusion zone
                    transform.position = relativePosition.y > transform.position.y ?
                        relativePosition : new Vector3(relativeX, transform.position.y);
                }

                Offset = transform.position - MouseToWorldPoint();
            }
        }

        /// <summary>
        /// Converts the mouse position on the UI Canvvas to points within the
        /// world.
        /// </summary>
        /// <returns>The world point of the mouse.</returns>
        private Vector3 MouseToWorldPoint ()
        {
            return Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        }
    }
}
