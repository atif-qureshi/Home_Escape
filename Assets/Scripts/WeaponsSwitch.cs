using UnityEngine;

public class WeaponsSwitch : MonoBehaviour
{
    public GameObject Glok;
    public GameObject knife;

    private WeaponInventory inventory;

    void Start()
    {
        inventory = GetComponent<WeaponInventory>();
        DisableAllWeapons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisableAllWeapons();
            Debug.Log("Hands selected");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.hasGlock)
        {
            DisableAllWeapons();
            Glok.SetActive(true);
            Debug.Log("Glok equipped");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.hasKnife)
        {
            DisableAllWeapons();
            knife.SetActive(true);
            Debug.Log("Knife equipped");
        }
    }

    void DisableAllWeapons()
    {
        if (Glok != null) Glok.SetActive(false);
        if (knife != null) knife.SetActive(false);
    }
}
