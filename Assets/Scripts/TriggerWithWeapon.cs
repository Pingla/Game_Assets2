using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationWithWeapon : MonoBehaviour
{
    [Header("Animation Settings")]
    public GameObject targetObject;   
    public string animationName;     

    private Animation animationComponent;

    void Start()
    {
        // Ensure the target object is set and has an Animation component
        if (targetObject != null)
        {
            animationComponent = targetObject.GetComponent<Animation>();
            if (animationComponent == null)
            {
                Debug.LogError($"No Animation component found on {targetObject.name}!");
            }
            else
            {
                Debug.Log($"Animation component found on {targetObject.name}.");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is tagged as "Weapon"
        if (other.CompareTag("Weapon"))
        {
            Debug.Log($"Weapon detected: {other.name}");

            // Play the animation if available
            if (animationComponent != null && animationComponent[animationName] != null)
            {
                animationComponent.Play(animationName);
                Debug.Log($"Playing animation '{animationName}' on {targetObject.name}");
            }
            else
            {
                Debug.LogWarning($"Animation '{animationName}' not found on {targetObject.name}!");
            }
        }
    }
}







