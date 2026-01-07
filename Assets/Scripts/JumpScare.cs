using UnityEngine;

public class JumpScare : MonoBehaviour
{
    [Header("JumpScare Setup")]
    public GameObject jumpScareObject;    // Jo object player ko darayega
    public AudioSource scareSound;        // Scare sound
    public float displayTime = 2f;        // Object kitni der visible rahe
    public bool cameraShake = true;       // Camera shake enable/disable
    public float shakeIntensity = 0.2f;   // Camera shake intensity

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;

            // Show the jumpscare object
            if (jumpScareObject != null)
                jumpScareObject.GetComponent<MeshRenderer>().enabled = true;

            // Play scare sound
            if (scareSound != null)
                scareSound.Play();

            // Optional: camera shake
            if (cameraShake && Camera.main != null)
                StartCoroutine(ShakeCamera());

            // Destroy object after some time
            if (jumpScareObject != null)
                Destroy(jumpScareObject, displayTime);

            // Destroy the trigger itself
            Destroy(gameObject, displayTime);
        }
    }

    // Camera shake coroutine
    private System.Collections.IEnumerator ShakeCamera()
    {
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < 0.3f) // shake duration
        {
            Camera.main.transform.localPosition = originalPos + Random.insideUnitSphere * shakeIntensity;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.localPosition = originalPos;
    }
}
