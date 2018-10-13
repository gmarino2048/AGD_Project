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
        private float ExclusionTop;

        // Screen Bounds
        private float ScreenTop;
        private float ScreenLeft;
        private float ScreenRight;

        private enum Position
        {
            Top,
            Bottom
        }

        private Position LastPosition;
        private Vector3 Offset;

        private void Start()
        {
            Collider = GetComponent<BoxCollider2D>();
            LastPosition = Position.Bottom;
            Shakes = 0;
            Offset = new Vector2(0, 0);
            GetBounds();
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

        private void GetBounds () {
            float exclusionHeight = ExclusionZone.bounds.center.y +
                                          ExclusionZone.bounds.extents.y;
            ExclusionTop = exclusionHeight + Collider.bounds.extents.y;

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

        private void OnMouseDown()
        {
            Offset = transform.position - MouseToWorldPoint();
            Debug.Log(Offset);
        }

        private void OnMouseDrag()
        {
            Vector3 relativePosition = MouseToWorldPoint() + Offset;
            relativePosition.z = 0;

            if (!Collider.IsTouching(ExclusionZone))
            {
                float newX = Mathf.Clamp(relativePosition.x, ScreenLeft, ScreenRight);
                float newY = Mathf.Clamp(relativePosition.y, ExclusionTop, ScreenTop);
                Vector3 newPosition = new Vector3(newX, newY, relativePosition.z);

                transform.position = newPosition;
            }
            else
            {
                transform.position = relativePosition.y > transform.position.y ?
                    relativePosition : new Vector3(relativePosition.x, transform.position.y);

                Offset = transform.position - MouseToWorldPoint();
            }
        }

        private Vector3 MouseToWorldPoint ()
        {
            return Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        }
    }
}
