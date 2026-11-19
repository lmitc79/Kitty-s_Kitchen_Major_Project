using UnityEngine;

using UnityEngine.InputSystem;


//gpt
public class TouchObjectSelect : MonoBehaviour
{
    public GameObject workButton;
    public GameObject cookEatButton;

    public Camera cam;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Touchscreen.current == null || cam == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = touch.position.ReadValue();

            Ray ray = cam.ScreenPointToRay(touchPosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Selected: " + hit.collider.gameObject.name);

                OnObjectSelected(hit.collider.gameObject);
            }
        }
    }

    void OnObjectSelected(GameObject selectedObject)
    {
        // Add your button functionality here.
        Debug.Log(selectedObject.name + " was pressed!");
        if (selectedObject.name == "DoorMat")
        {
            if (workButton != null)
            {
                workButton.SetActive(!workButton.activeSelf);
            }

        }
        if (selectedObject.name == "CookStation")
        {
            if (cookEatButton != null)
            {
                cookEatButton.SetActive(!cookEatButton.activeSelf);
            }

        }

    }

}
