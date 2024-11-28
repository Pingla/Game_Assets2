using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject door;         
    public string animationName;   

    private Animation animationComponent;

    void Start()
    {
        
        if (door != null)
        {
            animationComponent = door.GetComponent<Animation>();

            if (animationComponent == null)
            {
                Debug.LogError($"No Animation component found on {door.name}!");
            }
        }
        else
        {
            Debug.LogError("Door GameObject is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            if (animationComponent != null && animationComponent[animationName] != null)
            {
                // Play the specified animation
                animationComponent.Play(animationName);
                Debug.Log($"Playing animation '{animationName}' on {door.name}");
            }
            else
            {
                Debug.LogWarning($"Animation '{animationName}' not found on {door.name}!");
            }
        }
    }
}

