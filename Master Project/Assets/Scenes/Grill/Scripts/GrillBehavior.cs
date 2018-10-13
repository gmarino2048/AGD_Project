using UnityEngine;
using System.Collections;
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
    [SerializeField]
    public GrillObjectBehavior SausagePrefab;

    /// <summary>
    /// Sets the limits of the grill top and defines where the user can and
    /// cannot place the grill objects.
    /// </summary>
    void Start()
    {
        // TODO: Include sprite size into limit setting.
        SetLimits();
    }

    /// <summary>
    /// Handles proper placement of the sausages on the grill top. Places and
    /// runs individual sausages on the grill every time the player clicks 
    /// there.
    /// </summary>
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
                GrillObjectBehavior created = Instantiate(SausagePrefab);
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
