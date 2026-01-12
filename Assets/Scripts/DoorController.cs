using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    // Door Settings
    public Transform doorObject;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    // UI Text Settings
    public GameObject textDisplay;
    public string openMessage = "Press [E] to Open Door";
    public string closeMessage = "Press [E] to Close Door";

    // Audio Settings
    public AudioSource audioSource;   // Assign the door's AudioSource here
    public AudioClip openClip;        // Door open sound
    public AudioClip closeClip;       // Door close sound

    // Input Action
    private InputAction interactAction;

    // Private variables
    private bool isOpen = false;
    private bool playerIsNear = false;
    private Quaternion startRotation;
    private Quaternion endRotation;

    void Start()
    {
        // Set initial rotations
        startRotation = doorObject.rotation;
        endRotation = startRotation * Quaternion.Euler(0, openAngle, 0);

        // Hide text at start
        if (textDisplay != null)
            textDisplay.SetActive(false);

        // Setup new input system
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.performed += ctx => OnInteractPressed();
        interactAction.Enable();
    }

    void Update()
    {
        // Smooth door rotation
        if (isOpen)
        {
            doorObject.rotation = Quaternion.Slerp(
                doorObject.rotation,
                endRotation,
                Time.deltaTime * openSpeed
            );
        }
        else
        {
            doorObject.rotation = Quaternion.Slerp(
                doorObject.rotation,
                startRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    void OnInteractPressed()
    {
        if (playerIsNear)
        {
            if (!isOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;

        // Play open sound
        if (audioSource != null && openClip != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(openClip);
        }

        // Change text to close message
        if (textDisplay != null)
        {
            Text textComponent = textDisplay.GetComponent<Text>();
            if (textComponent != null)
                textComponent.text = closeMessage;
        }
    }

    void CloseDoor()
    {
        isOpen = false;

        // Play close sound
        if (audioSource != null && closeClip != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(closeClip);
        }

        // Change text to open message
        if (textDisplay != null)
        {
            Text textComponent = textDisplay.GetComponent<Text>();
            if (textComponent != null)
                textComponent.text = openMessage;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            // Show text
            if (textDisplay != null)
            {
                textDisplay.SetActive(true);
                Text textComponent = textDisplay.GetComponent<Text>();
                if (textComponent != null)
                    textComponent.text = isOpen ? closeMessage : openMessage;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            // Hide text
            if (textDisplay != null)
                textDisplay.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (interactAction != null)
        {
            interactAction.Disable();
            interactAction.Dispose();
        }
    }
}