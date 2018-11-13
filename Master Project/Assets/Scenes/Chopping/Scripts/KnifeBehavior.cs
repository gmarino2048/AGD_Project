using System.Collections;
using UnityEngine;

namespace Chopping
{
    public class KnifeBehavior : MonoBehaviour
    {

        #region Parameters

        [Header("Screen Information")]
        [SerializeField]
        public Camera SceneCamera; // The main camera within the scene
        public float ZIndex; // The action item's Z position in the scene

        public ScorekeeperBehavior Scorekeeper; // The scorekeeper object for the scene
        public TimerBehavior Timer;
        public ChopManager InputSource;

        [Header("Scene References")]
        [SerializeField]
        public ChopObjectBehavior ItemToChop; // The Choppable Object
        public float BufferWidth; // The width of the buffer to the end of the screen

        [Header("Knife Movement Attributes")]
        public float Velocity; // The speed with which the knife moves

        float LeftBound; // The left bound of the knife's movement
        float RightBound; // The right bound of the knife's movement

        bool Paused; // Tells the knife whether to move

        [Header("Animator Preferences")]
        [SerializeField]
        public Animator KnifeAnimator;
        public string PerformChop = "DoChop";


        /// <summary>
        /// The direction that the knife moves in.
        /// </summary>
        enum Direction
        {
            Left,
            Right
        }

        Direction CurrentDirection; // The knife object's current direction

        #endregion

        #region MonoBehaviour

        /// <summary>
        /// Called once at the start of the game.
        /// </summary>
        void Start()
        {
            // Set the inital position of the knife
            float initialX = transform.position.x;
            float initialY = transform.position.y;

            Paused = false;

            Vector3 knifePosition = new Vector3(initialX, initialY, ZIndex);
            gameObject.transform.position = knifePosition;

            // Set the bounds for motion
            SetBounds();
        }


        /// <summary>
        /// Called once every frame.
        /// </summary>
        void Update()
        {
            if (Timer && !Paused)
            {
                // Get the current values of the knife
                float currentX = transform.position.x;
                float currentY = transform.position.y;
                float currentZ = transform.position.z;

                // Get the next X position
                float newX = GetNextPosition(currentX, Time.deltaTime);

                // Set the knife position
                Vector3 newPosition = new Vector3(newX, currentY, currentZ);
                transform.position = newPosition;
            }
        }

        #endregion

        #region Auxiliary

        /// <summary>
        /// Pauses the knife movement for a set number of seconds.
        /// </summary>
        /// <returns>The IEnumerator allowing this to be a coroutine.</returns>
        /// <param name="seconds">The number of seconds to pause for.</param>
        public IEnumerator DoAnimation(int seconds)
        {
            Paused = true;
            KnifeAnimator.SetTrigger(PerformChop);
            yield return new WaitForSeconds(seconds);
            Paused = false;
        }

        /// <summary>
        /// Sets the left and right bounds for the knife's movement.
        /// </summary>
        void SetBounds()
        {
            // Calculate Vertical and Horizontal Extents of the camera
            float horiExtent = (SceneCamera.orthographicSize * Screen.width / Screen.height);

            LeftBound = SceneCamera.transform.position.x - horiExtent + BufferWidth;
            RightBound = SceneCamera.transform.position.x + horiExtent - BufferWidth;
        }

        /// <summary>
        /// Gets the knife's next position in its mvoement path.
        /// </summary>
        /// <returns>The next position of the knife.</returns>
        /// <param name="currentPosition">The knife's current X position.</param>
        /// <param name="elapsedTime">The elapsed time.</param>
        float GetNextPosition(float currentPosition, float elapsedTime)
        {
            // Check if we should switch direction
            if (currentPosition <= LeftBound)
            {
                CurrentDirection = Direction.Right;
            }
            else if (currentPosition >= RightBound)
            {
                CurrentDirection = Direction.Left;
            }

            // Move the knife
            float newX = CurrentDirection == Direction.Left
                ? MovementFunction(currentPosition, LeftBound - 1, elapsedTime)
                : MovementFunction(currentPosition, RightBound + 1, elapsedTime);
            return newX;
        }


        /// <summary>
        /// The function controlling the knife's movement pattern.
        /// </summary>
        /// <returns>The next position of the knife</returns>
        /// <param name="start">The knife path starting position.</param>
        /// <param name="stop">The knife path ending position.</param>
        /// <param name="time">The elapsed time.</param>
        float MovementFunction(float start, float stop, float time)
        {
            // TODO: Replace with an exponential smoothdamp function
            return start > stop
                    ? start - (Velocity * time)
                    : start + (Velocity * time);
        }

        #endregion
    }
}
