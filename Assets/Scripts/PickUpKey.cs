using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    public GameObject keyOB;
    public GameObject invOB;
    public AudioSource keySound;

    private bool inReach;

    void Start()
    {
        inReach = false;
        invOB.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            Debug.Log("Player in reach");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            Debug.Log("Player left reach");
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.P))
        {
            keyOB.SetActive(false);

            if (keySound != null)
                keySound.Play();

            invOB.SetActive(true);
            Debug.Log("Key picked up");
        }
    }
}
