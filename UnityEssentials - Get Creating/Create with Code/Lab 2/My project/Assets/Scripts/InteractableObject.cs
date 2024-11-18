using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
 private bool isPlayerInRange = false;
 private Renderer objectRenderer;
 private Material originalMaterial;
 public Material highlightMaterial; // highlight colour is assigned in the Inspector

public GameObject interactionPanel1; // Reference to the panel containing UI elements
private GameManager gameManager;
private bool isMessageDisplayed = false;
private bool hasBeenInteracted = false;


private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material; // Store the original material

        interactionPanel1.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isMessageDisplayed)
        {
            ShowInteractionUI();
        }
        else if (isMessageDisplayed && Input.GetKeyDown(KeyCode.E))
        {
            HideInteractionUI();
        }
    }



    private void ShowInteractionUI()
    {
        // Show the panel
        interactionPanel1.SetActive(true);
        isMessageDisplayed = true;

        // hide after 5 seconds
        // StartCoroutine(HideAfterDelay());
        gameManager.PauseGame();

        hasBeenInteracted = true;
        gameManager.ObjectInteracted(this);

    }

private void HideInteractionUI()
    {
        interactionPanel1.SetActive(false);
        isMessageDisplayed = false;
        gameManager.ResumeGame();
        gameManager.InteractionComplete();

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

        objectRenderer.material = highlight ? highlightMaterial : originalMaterial; // Revert to original material

    }

 
}
