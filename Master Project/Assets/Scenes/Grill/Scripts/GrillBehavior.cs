using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class GrillBehavior : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField]
    public Camera Main;
    public float TopBoundaries = 1f;
    public float SideBoundaries = 1f;

    public float Z_Index;

    public Rect Limits { get; private set; }

    [Header("Sausage Object")]
    // TODO: Edit this so that it creates a new sausage behavior object
    [SerializeField]
    public SausageBehavior SausagePrefab;

    void Start()
    {
        SetLimits();
    }

    void Update()
    {
        PlaceSausage();
    }

    /// <summary>
    /// Places a "sausage" on the grill when the user clicks. In this case,
    /// a sausage is any grillable object.
    /// </summary>
    void PlaceSausage()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (Limits.Contains(hit.point)){
                    SausageBehavior created = Instantiate(SausagePrefab);
                    created.transform.position = new Vector3(hit.point.x,
                                                             hit.point.y,
                                                             Z_Index);
                    // Do necessary stuff here
                }
            }
            else {
                Debug.Log("Position is outside allowed bounds");
            }
        }
        // Else do nothing
    }

    /// <summary>
    /// Sets the Rect object which defines the limits of the grill. Only
    /// grillable objects placed within this rectangle are valid.
    /// </summary>
    void SetLimits()
    {
        if (Main.orthographic)
        {
            // Calculate the extents of the camera view
            float verticalExtents = Main.orthographicSize;
            float horizontalExtents = Main.orthographicSize * (Screen.width /
                                                               Screen.height);

            // Get position of camera assuming looking in Z direction
            float x = Main.transform.position.x;
            float y = Main.transform.position.y;

            float width = horizontalExtents * 2;
            float height = verticalExtents * 2;

            Limits = new Rect(x, y, width, height);
        }
        else throw new UnityException("Camera Needs to be orthographic for " +
                                      "GrillBehavior");
    }
}
