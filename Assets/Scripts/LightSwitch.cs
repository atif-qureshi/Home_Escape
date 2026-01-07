using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject onOB;
    public GameObject offOB;
    public GameObject lightOB;

    public AudioSource switchClick;

    public bool lightsAreOn;
    public bool lightsAreOff;
    public bool inReach;

    void Start()
    {
        Debug.Log("🔌 LightSwitch script started");

        inReach = false;
        lightsAreOn = false;
        lightsAreOff = true;

        if (onOB == null || offOB == null || lightOB == null)
        {
            Debug.LogError("❌ One or more GameObjects are NOT assigned in Inspector!");
        }

        onOB.SetActive(false);
        offOB.SetActive(true);
        lightOB.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            Debug.Log("🟢 Player entered light switch range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            Debug.Log("🔴 Player left light switch range");
        }
    }

    void Update()
    {
        if (!inReach)
            return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("🔘 'S' key pressed");

            if (lightsAreOn)
            {
                Debug.Log("💡 Turning lights OFF");

                lightOB.SetActive(false);
                onOB.SetActive(false);
                offOB.SetActive(true);

                lightsAreOn = false;
                lightsAreOff = true;
            }
            else
            {
                Debug.Log("💡 Turning lights ON");

                lightOB.SetActive(true);
                onOB.SetActive(true);
                offOB.SetActive(false);

                lightsAreOn = true;
                lightsAreOff = false;
            }

            if (switchClick != null)
                switchClick.Play();
            else
                Debug.LogWarning("⚠️ switchClick AudioSource NOT assigned");
        }
    }
}
