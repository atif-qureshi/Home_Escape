using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    // Weapons
    public bool hasGlock = false;
    public bool hasKnife = false;

    // Keys
    private HashSet<string> keys = new HashSet<string>();

    // Medikits
    private List<int> medikits = new List<int>(); // Stores heal amounts

    // Reference to player health
    public PlayerHealth playerHealth;

    // --- Keys ---
    public void AddKey(string keyID)
    {
        keys.Add(keyID);
        Debug.Log("Key added: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }

    // --- Medikits ---
    public void AddMedikit(int healAmount)
    {
        medikits.Add(healAmount);
        Debug.Log("Medikit added! Total: " + medikits.Count);
    }

    public void UseMedikit()
    {
        if (medikits.Count == 0)
        {
            Debug.Log("No medikits to use!");
            return;
        }

        int healAmount = medikits[0]; // Take the first medikit
        medikits.RemoveAt(0);

        if (playerHealth != null)
            playerHealth.Heal(healAmount);

        Debug.Log("Medikit used! Remaining: " + medikits.Count);
    }

    void Update()
    {
        // Press L to use medikit
        if (Input.GetKeyDown(KeyCode.L))
        {
            UseMedikit();
        }
    }
}
