using UnityEngine;

using UnityEngine.InputSystem;

public class ScrollAndPinch : MonoBehaviour
{
    public Camera cam;
    public float dragSpeed = 1f;

    private Vector2 previousPosition;
    private bool isDragging;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (cam == null || Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.touches[0];

        if (touch.press.isPressed)
        {
            Vector2 currentPosition = touch.position.ReadValue();

            if (isDragging)
            {
                Vector2 difference = currentPosition - previousPosition;

                Vector3 movement = cam.transform.right * difference.x
                                 + cam.transform.up * difference.y;

                if (cam.orthographic)
                {
                    cam.transform.position -= movement *
                        (cam.orthographicSize / Screen.height) * dragSpeed;
                }
                else
                {
                    cam.transform.position -= movement *
                        (Vector3.Distance(cam.transform.position, Vector3.zero)
                        / Screen.height) * dragSpeed;
                }
            }

            previousPosition = currentPosition;
            isDragging = true;
        }
        else
        {
            isDragging = false;
            previousPosition = Vector2.zero;
        }
    }
}


    

