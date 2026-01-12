using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField] private GameObject noteUI;      // Panel jo note show karega
    [SerializeField] private Text noteText;          // Note ka legacy UI text
    [SerializeField] private Text pressEText;        // "Press E" UI text
    [TextArea(3, 10)]
    [SerializeField] private string content;         // Note ka text

    private bool isPlayerNear = false;

    void Start()
    {
        // Start me panel aur "Press E" hide karo
        noteUI.SetActive(false);
        pressEText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // Player pass hai → show Press E
            pressEText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                noteUI.SetActive(true);
                noteText.text = content;
                pressEText.gameObject.SetActive(false); // Hide Press E
            }
        }

        // Escape se note close karo
        if (noteUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            noteUI.SetActive(false);
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
            pressEText.gameObject.SetActive(false); // Player door gaya → hide Press E
        }
    }
}
