using UnityEngine;
using UnityEngine.UI;

public class BreakerBoxDoor : MonoBehaviour
{
    [Header("Door Setup")]
    public Animator doorAnim;          // Animator with "Open" trigger
    public AudioSource doorSound;      // Sound when door opens

    [Header("UI")]
    public Text infoText;              // Optional legacy text for hint
    public string hintText = "Press E to Open Door";

    [Header("Interaction")]
    public float interactDistance = 3f;

    private Transform player;
    private bool doorOpened = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (infoText != null)
            infoText.text = "";
    }

    void Update()
    {
        if (doorOpened) return;

        // Check distance to player
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactDistance)
        {
            if (infoText != null)
                infoText.text = hintText;

            // Press E to open
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
        else
        {
            if (infoText != null)
                infoText.text = "";
        }
    }

    void OpenDoor()
    {
        doorOpened = true;

        if (doorAnim != null)
            doorAnim.SetTrigger("Open");

        if (doorSound != null)
            doorSound.Play();

        // Clear hint text
        if (infoText != null)
            infoText.text = "";
    }
}
