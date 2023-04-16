using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractionManager : MonoBehaviour
{
    private Camera mainCamera; // Reference to the main camera

    private void Start()
    {
        mainCamera = Camera.main; // Get the main camera
    }

    private void Update()
    {
        // If the left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Cast a ray from the camera to the object
            if (Physics.Raycast(ray, out hit))
            {
                // Get the ClickHandler component and call its ObjectClicked method
                ClickHandler clickHandler = hit.transform.GetComponent<ClickHandler>();
                if (clickHandler != null)
                {
                    clickHandler.ObjectClicked();
                }

                // Get the DragHandler component and call its OnMouseDown method
                DragHandler dragHandler = hit.transform.GetComponent<DragHandler>();
                if (dragHandler != null)
                {
                    dragHandler.OnMouseDown();
                }
            }
        }
        // If the left mouse button is released
        else if (Input.GetMouseButtonUp(0))
        {
            // Get all DragHandler components and call their OnMouseUp method
            DragHandler[] dragHandlers = FindObjectsOfType<DragHandler>();
            foreach (DragHandler dragHandler in dragHandlers)
            {
                dragHandler.OnMouseUp();
            }
        }
    }
}
