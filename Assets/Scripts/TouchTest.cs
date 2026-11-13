using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchTest : MonoBehaviour
{
      private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        foreach(Touch touch in Touch.activeTouches)
        {
            Debug.Log($"finger {touch.finger.index} | Phase: {touch.phase} | Pos: {touch.screenPosition}");
        }
    }
}
