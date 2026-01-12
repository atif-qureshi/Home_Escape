using UnityEngine;

public class MedikitPickup : MonoBehaviour
{
    public int healAmount = 25;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponInventory inv = other.GetComponent<WeaponInventory>();
            if (inv != null)
            {
                inv.AddMedikit(healAmount);
                Destroy(gameObject); // remove medikit from scene
            }
        }
    }
}
