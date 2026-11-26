using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class GridSystem : MonoBehaviour
{
    
   
    [Header("3D Scene References")]
    public Camera mainCamera;
    public Transform floorPlane; // Drag your existing 3D Plane here
    public GameObject buildingPrefab; // 3D Prefab (must have a Collider)

    [Header("3D Grid Settings")]
    public int gridDivisions = 10; // 10x10 grid across the Plane
    public LayerMask floorLayer;   // Set to your Floor's layer

    private GameObject previewObject;
    private float objectHalfHeight = 0f;
    private float currentYRotation = 0f;
    private HashSet<Vector2Int> occupiedCells = new HashSet<Vector2Int>();

    void Update()
    {
        // Ensure a touchscreen exists on the device
        if (Touchscreen.current == null) return;

        // Check if more than 1 finger is touching (e.g., camera pinch/orbit)
        int activeTouches = 0;
        foreach (TouchControl t in Touchscreen.current.touches)
        {
            if (t.press.isPressed) activeTouches++;
        }
        if (activeTouches > 1)
        {
            CancelPreview();
            return;
        }

        TouchControl primaryTouch = Touchscreen.current.primaryTouch;
        TouchPhase phase = primaryTouch.phase.ReadValue();

        // Exit if no touch activity is happening this frame
        if (phase == TouchPhase.None) return;

        // 1. Ignore touches over UI buttons using the touch's unique ID
        int touchId = primaryTouch.touchId.ReadValue();
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touchId))
            return;

        // 2. 3D Raycast hitting ONLY the floor layer
        Vector2 touchPosition = primaryTouch.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, floorLayer) && hit.transform == floorPlane)
        {
            // 3. Convert 3D world hit to the Plane's local space (-5 to +5)
            Vector3 localHit = floorPlane.InverseTransformPoint(hit.point);

            float normalizedX = localHit.x + 5f;
            float normalizedZ = localHit.z + 5f;
            float localCellSize = 10f / gridDivisions;

            int cellX = Mathf.Clamp(Mathf.FloorToInt(normalizedX / localCellSize), 0, gridDivisions - 1);
            int cellZ = Mathf.Clamp(Mathf.FloorToInt(normalizedZ / localCellSize), 0, gridDivisions - 1);
            Vector2Int cellPos = new Vector2Int(cellX, cellZ);

            // 4. Calculate snapped 3D position on the floor
            Vector3 snappedLocalPos = new Vector3(
                (cellX * localCellSize) + (localCellSize * 0.5f) - 5f,
                0f,
                (cellZ * localCellSize) + (localCellSize * 0.5f) - 5f
            );

            Vector3 snappedWorldPos = floorPlane.TransformPoint(snappedLocalPos);
            snappedWorldPos += floorPlane.up * objectHalfHeight;

            HandleTouchPhase(phase, cellPos, snappedWorldPos);
        }
    }

    private void HandleTouchPhase(TouchPhase phase, Vector2Int cellPos, Vector3 worldPos)
    {
        if (phase == TouchPhase.Began)
        {
            Quaternion spawnRot = floorPlane.rotation * Quaternion.Euler(0f, currentYRotation, 0f);
            previewObject = Instantiate(buildingPrefab, worldPos, spawnRot);

            if (previewObject.TryGetComponent(out Collider col))
            {
                objectHalfHeight = col.bounds.extents.y;
                col.enabled = false;
                previewObject.transform.position = worldPos + (floorPlane.up * objectHalfHeight);
            }
        }
        else if (phase == TouchPhase.Moved && previewObject != null)
        {
            previewObject.transform.position = worldPos;
        }
        else if ((phase == TouchPhase.Ended || phase == TouchPhase.Canceled) && previewObject != null)
        {
            if (!occupiedCells.Contains(cellPos))
            {
                if (previewObject.TryGetComponent(out Collider col))
                    col.enabled = true;

                occupiedCells.Add(cellPos);
            }
            else
            {
                Destroy(previewObject);
            }
            previewObject = null;
        }
    }

    public void RotateBuilding90Degrees()
    {
        currentYRotation = (currentYRotation + 90f) % 360f;
        if (previewObject != null)
        {
            previewObject.transform.rotation = floorPlane.rotation * Quaternion.Euler(0f, currentYRotation, 0f);
        }
    }

    private void CancelPreview()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
            previewObject = null;
        }
    }
}

//{
//    public GameObject objectToPlace;
//    public float gridSize = 1f;
//    private GameObject ghostObject;
//    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        CreateGhostObject();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        UpdateGhostPosition();

//        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
//        {
//            PlaceObject();
//        }
//    }

//    void CreateGhostObject()
//    {
//        ghostObject = Instantiate(objectToPlace);
//        ghostObject.GetComponent<Collider>().enabled = false;

//        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

//        foreach (Renderer renderer in renderers)
//        {
//            Material mat = renderer.material;
//            Color color = mat.color;
//            color.a = 0.5f;
//            mat.color = color;

//            mat.SetFloat("_Mode", 2);
//            mat.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//            mat.SetInt("_stBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//            mat.SetInt("_ZWrite", 0);
//            mat.DisableKeyword("_ALPHATEST_ON");
//            mat.EnableKeyword("_ALPHABLEND_ON");
//            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
//            mat.renderQueue = 3000;
//        }
//    }

//    void UpdateGhostPosition()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        if(Physics.Raycast(ray, out RaycastHit hit))
//        {
//            Vector3 point = hit.point;

//            Vector3 snappedPosition = new Vector3(
//                Mathf.Round(point.x / gridSize) * gridSize, 
//                Mathf.Round(point.y / gridSize) * gridSize, 
//                Mathf.Round(point.z / gridSize) * gridSize
//                );

//            ghostObject.transform.position = snappedPosition;

//            if (occupiedPositions.Contains(snappedPosition))
//                    SetGhostColor(Color.red);
//            else
//                SetGhostColor(new Color());
//        }
//    }

//    void SetGhostColor(Color color)
//    {
//        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

//        foreach(Renderer renderer in renderers)
//        {
//            Material mat = renderer.material;
//            mat.color = color;
//        }
//    }

//    void PlaceObject()
//    {
//        Vector3 placementPosition = ghostObject.transform.position;
//        if(!occupiedPositions.Contains(placementPosition))
//        {
//            Instantiate(objectToPlace, placementPosition, Quaternion.identity);

//            occupiedPositions.Add(placementPosition);
//        }

//    }


//}
