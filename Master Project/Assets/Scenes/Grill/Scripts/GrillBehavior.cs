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

    public BoxCollider Limits { get; private set; }

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
                SausageBehavior created = Instantiate(SausagePrefab);
                created.transform.position = new Vector3(hit.point.x,
                                                         hit.point.y,
                                                         Z_Index);
                    // Do necessary stuff here
                
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
        if (Main.orthographic){
            float verticalExtents = Main.orthographicSize;
            float horizontalExtents = Main.orthographicSize * Screen.width /
                                                              Screen.height;
            Debug.Log(Screen.height + " " + Screen.width);
            Vector3 centerPosition = new Vector3(Main.transform.position.x,
                                                 Main.transform.position.y,
                                                 transform.position.z);

            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.center = centerPosition;
            boxCollider.size = new Vector3((2 * horizontalExtents) - (2 * TopBoundaries),
                                           (2 * verticalExtents) - (2 * SideBoundaries),
                                           0.01f);
        }
    }
}
