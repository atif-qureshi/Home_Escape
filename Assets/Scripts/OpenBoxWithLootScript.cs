using UnityEngine;

public class OpenBoxWithLootScript : MonoBehaviour
{
    public Animator boxOB;
    public GameObject keyOBNeeded;
    public AudioSource openSound;

    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject drop4;
    public GameObject drop5;
    public GameObject drop6;

    private bool inReach;
    private bool isOpen;

    private int randomNumber;

    void Start()
    {
        randomNumber = Random.Range(0, 6); // 0 to 5
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            Debug.Log("Player in reach of loot box");
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
            OpenBox();
        }
        else if (!keyOBNeeded.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key missing");
        }
    }

    void OpenBox()
    {
        keyOBNeeded.SetActive(false);
        openSound.Play();
        boxOB.SetBool("open", true);
        isOpen = true;

        switch (randomNumber)
        {
            case 0: drop1.SetActive(true); break;
            case 1: drop2.SetActive(true); break;
            case 2: drop3.SetActive(true); break;
            case 3: drop4.SetActive(true); break;
            case 4: drop5.SetActive(true); break;
            case 5: drop6.SetActive(true); break;
        }

        boxOB.GetComponent<BoxCollider>().enabled = false;
        boxOB.GetComponent<OpenBoxWithLootScript>().enabled = false;

        Debug.Log("Box opened, loot dropped: " + randomNumber);
    }
}
