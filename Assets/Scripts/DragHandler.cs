using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging;
    private Vector3 startingPosition;
    private bool isAtStartingPosition = true;
    private bool isOnPlane = false;
    private float smoothSpeed = 10f;
    private Renderer characterRenderer; // Reference to the character's Renderer component
    private Color originalColor; // Store the original material color

    private void Start()
    {
        mainCamera = Camera.main;
        startingPosition = transform.position;
        //characterRenderer = GetComponent<Renderer>(); // Get the Renderer component
       // originalColor = characterRenderer.material.color; // Store the original material color
    }

    private void Update()
    {
        if (isDragging)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 targetPosition = new Vector3(hit.point.x + offset.x, transform.position.y, hit.point.z + offset.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            }
        }
    }

    public void OnMouseDown()
    {
        // Check if the character is not at the starting position, is on the plane, and can't be picked up from the plane
        if ((!isAtStartingPosition || isOnPlane) && !PickUpController.CanPickUpFromPlane) return;

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            offset = transform.position - hit.point;
            isDragging = true;
            Debug.Log("Dragging character");
          //  SetVisualFeedback(true); // Enable visual feedback
        }
    }

    public void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Debug.Log("Character dropped on the plane");
                isOnPlane = true;
            }
            else
            {
                transform.position = startingPosition;
                Debug.Log("Character not dropped on the plane, returning to starting position");
                isOnPlane = false;
            }
            isAtStartingPosition = true;
          //  SetVisualFeedback(false); // Disable visual feedback
        }
    }

    // Set visual feedback by changing the material color
    private void SetVisualFeedback(bool active)
    {
        if (active)
        {
            characterRenderer.material.color = Color.green; // Change the material color when picked up
        }
        else
        {
            characterRenderer.material.color = originalColor; // Reset the material color when dropped
        }
    }
}
