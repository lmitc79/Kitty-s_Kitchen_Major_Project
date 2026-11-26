using UnityEngine;

public class RenoButton : MonoBehaviour
{
    public GameObject scrollReferenceObject;
    public GameObject floorPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleScreenScroll()
    {
            scrollReferenceObject.SetActive(!scrollReferenceObject.activeSelf);
    }

    public void ToggleGridPlacement()
    {
        floorPlane.SetActive(!floorPlane.activeSelf);
    }
}
