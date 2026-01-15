using UnityEngine;
using TMPro;
using System.Collections;

public class PickUpItem : MonoBehaviour
{
    public string itemName;
    public string keyID;

    [Header("UI")]
    public TextMeshProUGUI pickupText;
    public float textDisplayTime = 2f;
    public GameObject inventoryUI;

    [Header("Audio")]
    public AudioSource pickupSound;

    private bool inReach = false;
    private bool isPicked = false;

    void Start()
    {
        if (pickupText != null)
            pickupText.gameObject.SetActive(false);

        if (inventoryUI != null)
            inventoryUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isPicked)
            return;

        inReach = true;

        // 🔹 SHOW APPROACH TEXT
        if (pickupText != null)
        {
            pickupText.text = "Press P to pick up " + itemName;
            pickupText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        inReach = false;

        // 🔹 HIDE TEXT
        if (pickupText != null)
            pickupText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (inReach && !isPicked && Input.GetKeyDown(KeyCode.P))
            CollectItem();
    }

    void CollectItem()
    {
        isPicked = true;

        if (pickupSound != null)
            pickupSound.Play();

        // 🔹 CHANGE TEXT AFTER PICKUP
        if (pickupText != null)
            StartCoroutine(ShowPickupText());

        if (inventoryUI != null)
            inventoryUI.SetActive(true);

        WeaponInventory inv =
            GameObject.FindWithTag("Player").GetComponent<WeaponInventory>();

        if (inv != null)
        {
            if (!string.IsNullOrEmpty(keyID))
                inv.AddKey(keyID);

            if (itemName == "Glok")
                inv.hasGlock = true;

            if (itemName == "Knife")
                inv.hasKnife = true;
        }

        HideItem();
    }

    IEnumerator ShowPickupText()
    {
        pickupText.text = "Picked up " + itemName;
        pickupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(textDisplayTime);

        pickupText.gameObject.SetActive(false);
    }

    void HideItem()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }
}
