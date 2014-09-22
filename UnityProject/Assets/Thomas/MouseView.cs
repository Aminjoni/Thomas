using UnityEngine;

public class MouseView : MonoBehaviour 
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
 
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            Debug.Log(dragOrigin);
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;
 
        //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Debug.Log(Input.mousePosition-dragOrigin);
        Debug.Log(Camera.mainCamera.ScreenToViewportPoint(Input.mousePosition));


        //Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
 
        //transform.Translate(move, Space.World);  
    }
 
 
}