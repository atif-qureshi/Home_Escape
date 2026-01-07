using UnityEngine;

public class OpenBoxScript : MonoBehaviour
{
    public Animator boxOB;
    public GameObject keyOBNeeded;
    public AudioSource openSound;

    private bool inReach;
    private bool isOpen;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            Debug.Log("Player in reach of box");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
        }
    }

    void Update()
    {
        if (!inReach || isOpen) return;

        if (keyOBNeeded.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            keyOBNeeded.SetActive(false);
            openSound.Play();
            boxOB.SetBool("open", true);
            isOpen = true;
            Debug.Log("Box opened");
        }
    }
}
