using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorsWithLock : MonoBehaviour
{
    // Door Settings
    public Transform doorObject;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    // UI
    public GameObject textDisplay;
    public string openMessage = "Press [E] to Open Door";
    public string closeMessage = "Press [E] to Close Door";
    public string lockedMessage = "Door is Locked";

    // Audio
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip lockedClip;

    // Input
    private InputAction interactAction;

    // State
    private bool isOpen = false;
    private bool playerIsNear = false;
    private bool isDestroying = false; // Add flag to track destruction state

    // Key system
    public string requiredKeyID = "Key1"; // Key needed to open this door
    private WeaponInventory playerInventory;

    private Quaternion startRotation;
    private Quaternion endRotation;

    void Start()
    {
        startRotation = doorObject.rotation;
        endRotation = startRotation * Quaternion.Euler(0, openAngle, 0);

        if (textDisplay != null)
            textDisplay.SetActive(false);

        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.performed += OnInteractPressed;
        interactAction.Enable();

        // Find player inventory
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerInventory = player.GetComponent<WeaponInventory>();
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        if (!playerIsNear)
            return;

        // Check if player has the key
        if (playerInventory == null || !playerInventory.HasKey(requiredKeyID))
        {
            PlaySound(lockedClip);
            ShowText(lockedMessage);
            return;
        }

        if (!isOpen)
            OpenDoor();
        else
            CloseDoor();
    }

    void Update()
    {
        // Smoothly rotate the door
        Quaternion targetRotation = isOpen ? endRotation : startRotation;
        doorObject.rotation = Quaternion.Slerp(
            doorObject.rotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );
    }

    void OpenDoor()
    {
        isOpen = true;
        PlaySound(openClip);
        ShowText(closeMessage);
    }

    void CloseDoor()
    {
        isOpen = false;
        PlaySound(closeClip);
        ShowText(openMessage);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
        }
    }

    void ShowText(string msg)
    {
        if (textDisplay == null) return;

        textDisplay.SetActive(true);

        TMP_Text t = textDisplay.GetComponent<TMP_Text>();
        if (t != null)
            t.text = msg;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            playerIsNear = true;
            ShowText(isOpen ? closeMessage : openMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            playerIsNear = false;
            if (textDisplay != null)
                textDisplay.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (isDestroying) return;
        isDestroying = true;

        try
        {
            // IMPORTANT: Unsubscribe from the event first
            if (interactAction != null)
            {
                // Unsubscribe using the same method signature
                interactAction.performed -= OnInteractPressed;

                // Disable and dispose safely
                if (interactAction.enabled)
                    interactAction.Disable();

                interactAction.Dispose();
                interactAction = null; // Clear reference
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error during door cleanup: {e.Message}");
        }
    }

    // Alternative: Use OnDisable() which is more reliable than OnDestroy()
    void OnDisable()
    {
        // This will be called when the object is disabled or destroyed
        CleanupInputAction();
    }

    private void CleanupInputAction()
    {
        if (interactAction == null) return;

        try
        {
            // Unsubscribe from the event
            interactAction.performed -= OnInteractPressed;

            // Only disable if currently enabled
            if (interactAction.enabled)
            {
                interactAction.Disable();
            }

            // Dispose if not already disposed
            if (!interactAction.Equals(default(InputAction)))
            {
                interactAction.Dispose();
            }
        }
        catch (System.ObjectDisposedException)
        {
            // Already disposed, ignore
        }
        finally
        {
            interactAction = null;
        }
    }
}



