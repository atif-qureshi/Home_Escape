using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 10f;
    public AudioClip alertSound;          // Sound to play when player is detected
    public bool loopSound = true;         // Optional: loop the sound while player is in range

    private Transform player;
    private AudioSource audioSource;
    private bool isPlayerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Add/get AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = alertSound;
        audioSource.loop = loopSound;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Move towards player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(player);

            // Start sound if not already playing
            if (!isPlayerInRange)
            {
                if (alertSound != null)
                    audioSource.Play();
                isPlayerInRange = true;
            }
        }
        else
        {
            // Player left range, stop sound if looping
            if (isPlayerInRange)
            {
                if (loopSound && audioSource.isPlaying)
                    audioSource.Stop();
                isPlayerInRange = false;
            }
        }
    }
}
