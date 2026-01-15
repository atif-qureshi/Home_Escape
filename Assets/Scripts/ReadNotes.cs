using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    public GameObject player;       // The player object
    public GameObject noteUI;       // UI that shows the note
    public GameObject pickUpText;   // "Press E to pick up" text
    public AudioSource pickUpSound; // Sound when picking up
    public bool inReach;            // Is player close enough?

    private CharacterController playerController;

    void Start()
    {
        noteUI.SetActive(false);
        pickUpText.SetActive(false);

        inReach = false;

        // Get the CharacterController component
        playerController = player.GetComponent<CharacterController>();
        if (playerController == null)
        {
            Debug.LogWarning("CharacterController not found on player!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            noteUI.SetActive(true);
            pickUpSound.Play();

            // Disable the pickup text
            pickUpText.SetActive(false);


            // Disable player movement
            if (playerController != null)
                playerController.enabled = false;

            // Show cursor for reading note
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitButton()
    {
        noteUI.SetActive(false);

        // Re-enable player movement
        if (playerController != null)
            playerController.enabled = true;

        // Hide cursor when exiting
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
