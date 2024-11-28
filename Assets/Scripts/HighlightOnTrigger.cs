using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnTrigger : MonoBehaviour
{
    public GameObject targetObject;       
    public Material highlightMaterial;   
    public Material originalMaterial;    

    private Renderer targetRenderer;     

    void Start()
    {
        
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                originalMaterial = targetRenderer.material; 
            }
            else
            {
                Debug.LogError("No Renderer found on the target object.");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player") && targetRenderer != null)
        {
            targetRenderer.material = highlightMaterial; // Change to highlight material
            Debug.Log("Player entered trigger, highlighting object.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the trigger
        if (other.CompareTag("Player") && targetRenderer != null)
        {
            targetRenderer.material = originalMaterial; // Revert to original material
            Debug.Log("Player exited trigger, removing highlight.");
        }
    }
}

