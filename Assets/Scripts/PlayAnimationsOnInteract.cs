using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationsOnInteract : MonoBehaviour
{
    public GameObject chest;            
    public GameObject key;              

    public string chestAnimationName = "ChestOpen"; 
    public string keyAnimationName = "Key Rotation"; 

    private Animation chestAnimationComponent;
    private Animation keyAnimationComponent;

    private bool playerInTrigger = false; 

    void Start()
    {
        
        if (chest != null)
        {
            chestAnimationComponent = chest.GetComponent<Animation>();
            if (chestAnimationComponent == null)
            {
                Debug.LogError($"No Animation component found on {chest.name}!");
            }
        }
        else
        {
            Debug.LogError("Chest GameObject is not assigned!");
        }

        if (key != null)
        {
            keyAnimationComponent = key.GetComponent<Animation>();
            if (keyAnimationComponent == null)
            {
                Debug.LogError($"No Animation component found on {key.name}!");
            }
        }
        else
        {
            Debug.LogError("Key GameObject is not assigned!");
        }
    }

    void Update()
    {
        
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PlayAnimations();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the trigger
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            Debug.Log("Player exited trigger zone.");
        }
    }

    private void PlayAnimations()
    {
        // Play the chest animation
        if (chestAnimationComponent != null && chestAnimationComponent[chestAnimationName] != null)
        {
            chestAnimationComponent.Play(chestAnimationName);
            Debug.Log($"Playing chest animation: {chestAnimationName}");
        }
        else
        {
            Debug.LogWarning($"Chest animation '{chestAnimationName}' could not be played!");
        }

        // Play the key animation
        if (keyAnimationComponent != null && keyAnimationComponent[keyAnimationName] != null)
        {
            keyAnimationComponent.Play(keyAnimationName);
            Debug.Log($"Playing key animation: {keyAnimationName}");
        }
        else
        {
            Debug.LogWarning($"Key animation '{keyAnimationName}' could not be played!");
        }
    }
}
