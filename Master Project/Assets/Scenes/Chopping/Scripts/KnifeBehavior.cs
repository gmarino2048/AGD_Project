using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{

    #region Parameters

    [Header("Screen Information")]
    [SerializeField]
    public Camera SceneCamera;
    public float XOffset;
    public float YOffset;
    public float ZIndex;

    [Header("Scene References")]
    [SerializeField]
    public ChopObjectBehavior ItemToChop;
    public float BufferWidth;

    [Header("Knife Movement Attributes")]
    public float Velocity;

    float LeftBound;
    float RightBound;

    enum Direction
    {
        Left,
        Right
    }

    Direction CurrentDirection;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        // Set the inital position of the knife
        float initialX = SceneCamera.transform.position.x + XOffset;
        float initialY = SceneCamera.transform.position.y + YOffset;

        Vector3 knifePosition = new Vector3(initialX, initialY, ZIndex);
        gameObject.transform.position = knifePosition;

        // Set the bounds for motion
        SetBounds();
    }

    void Update()
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

    #endregion

    #region Auxiliary

    void SetBounds()
    {
        // Calculate Vertical and Horizontal Extents of the camera
        float horiExtent = (SceneCamera.orthographicSize * Screen.width / Screen.height);

        LeftBound = SceneCamera.transform.position.x - horiExtent + BufferWidth;
        RightBound = SceneCamera.transform.position.x + horiExtent - BufferWidth;
    }

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

    float MovementFunction(float start, float stop, float time)
    {
        // TODO: Replace with an exponential smoothdamp function
        return start > stop
                ? start - (Velocity * time)
                : start + (Velocity * time);
    }

    #endregion
}
