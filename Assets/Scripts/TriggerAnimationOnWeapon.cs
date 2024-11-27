using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationOnWeapon : MonoBehaviour
{
    [Header("Animation Settings")]
    public GameObject objectWithAnimation;  
    public string animationName;           

    private Animation animationComponent;

    void Start()
    {
        
        if (objectWithAnimation != null)
        {
            animationComponent = objectWithAnimation.GetComponent<Animation>();

            if (animationComponent == null)
            {
                Debug.LogError($"No Animation component found on {objectWithAnimation.name}!");
            }
        }
        else
        {
            Debug.LogError("Object with animation is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get the root object of the collider that entered
        GameObject rootObject = other.transform.root.gameObject;

        // Check if the root object is tagged as "Weapon"
        if (rootObject.CompareTag("Weapon"))
        {
            Debug.Log($"Weapon detected: {rootObject.name}");

            // Play the animation if it's valid
            if (animationComponent != null && animationComponent[animationName] != null)
            {
                animationComponent.Play(animationName);
                Debug.Log($"Playing animation '{animationName}' on {objectWithAnimation.name}");
            }
            else
            {
                Debug.LogWarning($"Animation '{animationName}' not found or Animation component is missing on {objectWithAnimation.name}!");
            }
        }
        else
        {
            Debug.Log($"{rootObject.name} is not tagged as 'Weapon'.");
        }
    }
}




