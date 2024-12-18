using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnTrigger : MonoBehaviour
{
    public Text uiText;        
    public string message;     
    public float displayTime = 3f; 

    private bool isShowing = false; 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player") && !isShowing)
        {
            isShowing = true;
            StartCoroutine(DisplayMessage());
        }
    }

    private System.Collections.IEnumerator DisplayMessage()
    {
        // Show the text and set its message
        if (uiText != null)
        {
            uiText.text = message;
            uiText.gameObject.SetActive(true);

            // Wait for the specified display time
            yield return new WaitForSeconds(displayTime);

            // Hide the text
            uiText.gameObject.SetActive(false);
            isShowing = false;
        }
    }
}

