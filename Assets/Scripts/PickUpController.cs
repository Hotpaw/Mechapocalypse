using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    // Static property to control if the character can be picked up from the plane
    public static bool CanPickUpFromPlane { get; private set; } = true;

    // Method to toggle the pick-up behavior of the character from the plane
    public void TogglePickUpFromPlane()
    {
        CanPickUpFromPlane = !CanPickUpFromPlane;
    }
}
