using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName;        // "Key", "M4", etc.
    public GameObject inventoryUI; // Icon to show in HUD
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
        {
            inReach = true;
            Debug.Log("Player in reach of " + itemName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            Debug.Log("Player left reach of " + itemName);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.P))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        if (pickupSound != null)
            pickupSound.Play();

        if (inventoryUI != null)
            inventoryUI.SetActive(true);

        gameObject.SetActive(false); // Remove from scene
        Debug.Log(itemName + " picked up");

        // Optional: notify inventory system
        WeaponInventory playerInventory = GameObject.FindWithTag("Player").GetComponent<WeaponInventory>();
        if (playerInventory != null)
        {
            switch (itemName)
            {
                case "Key": playerInventory.hasKey = true; break;
                case "Glok": playerInventory.hasGlock  = true; break;
                case "Knife": playerInventory.hasKnife = true; break;
            }
        }
    }
}
