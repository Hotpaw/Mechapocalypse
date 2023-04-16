using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private string objectName; // The name of the object, used in the debug log

    // This method is called when the object is clicked
    public void ObjectClicked()
    {
        Debug.Log($"{objectName} clicked!");
    }
}

