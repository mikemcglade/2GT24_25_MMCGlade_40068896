using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
 private bool isPlayerInRange = false;
 private Renderer objectRenderer;
 private Material originalMaterial;
 public Material highlightMaterial; // Assign this in the Inspector

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material; // Store the original material
    }

    private void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        // Add additional interaction logic here (e.g., opening doors, picking up items)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            isPlayerInRange = true;
            HighlightObject(true); // Highlight when player enters range
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            HighlightObject(false); // Remove highlight when player exits range
        }
    }

    private void HighlightObject(bool highlight)
    {
        if (highlight)
        {
            objectRenderer.material = highlightMaterial; // Change to highlight material
        }
        else
        {
            objectRenderer.material = originalMaterial; // Revert to original material
        }
    }
}
