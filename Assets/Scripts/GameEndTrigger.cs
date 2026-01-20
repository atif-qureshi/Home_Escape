using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject endCanvas; // Canvas to show when game ends

    [Header("Audio")]
    public AudioSource endSound; // Sound to play at game end

    private bool gameEnded = false;

    void Start()
    {
        if (endCanvas != null)
            endCanvas.SetActive(false); // Hide canvas at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (other.CompareTag("Player"))
        {
            EndGame(other.gameObject);
        }
    }

    void EndGame(GameObject player)
    {
        gameEnded = true;

        // Show end canvas
        if (endCanvas != null)
            endCanvas.SetActive(true);

        // Play sound
        if (endSound != null)
            endSound.Play();

        // 🔹 Disable all movement scripts on the player
        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) // Don’t disable this trigger
                script.enabled = false;
        }

        // Optional: Freeze physics movement if Rigidbody exists
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
