using UnityEngine;

public class BreakerBox : MonoBehaviour
{
    [SerializeField] private Light[] controlledLights; // Jo lights breaker control kare
    [SerializeField] private GameObject pressEText;    // Optional: Press E UI text

    private bool isPlayerNear = false;

    void Start()
    {
        // Start me Press E hide karo
        if (pressEText != null)
            pressEText.SetActive(false);

        // Lights off start me
        foreach (Light l in controlledLights)
        {
            l.enabled = false;
        }
    }

    void Update()
    {
        if (isPlayerNear)
        {
            if (pressEText != null)
                pressEText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleLights();
            }
        }

        if (!isPlayerNear && pressEText != null)
        {
            pressEText.SetActive(false);
        }
    }

    void ToggleLights()
    {
        foreach (Light l in controlledLights)
        {
            l.enabled = !l.enabled; // On/Off toggle
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
