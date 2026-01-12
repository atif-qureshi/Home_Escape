using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;
    public string keyID;           // ← ADD THIS
    public GameObject inventoryUI;
    public AudioSource pickupSound;

    private bool inReach = false;

    void Start()
    {
        if (inventoryUI != null)
            inventoryUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = false;
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.P))
            CollectItem();
    }

    void CollectItem()
    {
        if (pickupSound != null)
            pickupSound.Play();

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

        gameObject.SetActive(false);
    }
}
