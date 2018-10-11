using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shaking
{
    public class ShakerBehavior : MonoBehaviour
    {

        public int Shakes { get; private set; } // The number of shakes completed by this object.
        public BoxCollider2D Collider { get; private set; }

        [Header("Bounding Zones")]
        [SerializeField]
        public BoxCollider2D TopBound; // The top bound that the shaker must enter.
        [SerializeField]
        public BoxCollider2D BottomBound; // The lower bound that the shaker must enter.

        [SerializeField]
        public BoxCollider2D ExclusionZone; // The zone that you can't enter.

        private enum Position
        {
            Top,
            Bottom
        }

        private bool Excluded;
        private Position LastPosition;
        private Vector3 Offset;

        private void Start()
        {
            Collider = GetComponent<BoxCollider2D>();
            Excluded = false;
            LastPosition = Position.Bottom;
            Shakes = 0;
            Offset = new Vector2(0, 0);
        }

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

        private void OnMouseDown()
        {
            Offset = transform.position - MouseToWorldPoint();
            Debug.Log(Offset);
        }

        private void OnMouseDrag()
        {
            Vector3 relativePosition = MouseToWorldPoint() + Offset;
            relativePosition.z = 0;
            transform.position = relativePosition;
        }

        private Vector3 MouseToWorldPoint ()
        {
            return Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        }
    }
}
