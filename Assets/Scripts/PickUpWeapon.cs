using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public Transform weaponHoldPosition;  // Where the weapon will be held
    public KeyCode pickUpKey = KeyCode.E; // Key to pick up and drop the weapon

    private GameObject heldWeapon = null; // The currently held weapon

    void Update()
    {
        // Check if the player presses the pick-up/drop key
        if (Input.GetKeyDown(pickUpKey))
        {
            if (heldWeapon == null)
            {
                TryPickUpWeapon();
            }
            else
            {
                DropWeapon();
            }
        }
    }

    void TryPickUpWeapon()
    {
        // Cast a ray forward to detect a weapon
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            // Get the root object in case the ray hits a child collider
            GameObject hitObject = hit.collider.transform.root.gameObject;

            // Check if the root object is tagged as "Weapon"
            if (hitObject.CompareTag("Weapon"))
            {
                PickUpWeaponObject(hitObject);
            }
            else
            {
                Debug.Log("Raycast hit an object, but it is not tagged as 'Weapon'.");
            }
        }
    }

    void PickUpWeaponObject(GameObject weapon)
    {
        // Set the held weapon
        heldWeapon = weapon;

        // Disable weapon physics
        Rigidbody rb = heldWeapon.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Move the weapon to the hold position
        heldWeapon.transform.position = weaponHoldPosition.position;
        heldWeapon.transform.rotation = weaponHoldPosition.rotation;

        // Parent the weapon to the hold position
        heldWeapon.transform.SetParent(weaponHoldPosition);

        Debug.Log("Weapon picked up: " + heldWeapon.name);
    }

    void DropWeapon()
    {
        if (heldWeapon != null)
        {
            // Unparent the weapon
            heldWeapon.transform.SetParent(null);

            // Re-enable weapon physics
            Rigidbody rb = heldWeapon.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Weapon dropped: " + heldWeapon.name);

            // Clear the held weapon reference
            heldWeapon = null;
        }
    }
}


