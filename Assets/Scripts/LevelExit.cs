using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // agar tum TextMeshPro use kar rahe ho

public class LevelExit : MonoBehaviour
{
    public string nextLevelName; // Next scene ka naam
    public GameObject promptText; // UI Text object jo show hoga

    private void Start()
    {
        if (promptText != null)
            promptText.SetActive(false); // initially hide text
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // sirf player pe trigger
        {
            if (promptText != null)
                promptText.SetActive(true); // show "Press E" text

            // Automatically load next level after 2 seconds (adjustable)
            Invoke("LoadNextLevel", 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
                promptText.SetActive(false); // hide text jab player bahar jaye
        }
    }

    void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Next level name is empty!");
        }
    }
}
