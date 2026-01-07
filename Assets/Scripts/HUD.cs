using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("Flashlight HUD")]
    public GameObject flashLightON;
    public GameObject flashLightOFF;
    public GameObject flashLightOB;

    [Header("Weapon HUD Icons")]
    public GameObject m4Icon;
    public GameObject knifeIcon;
    public GameObject handIcon;

    [Header("Weapon Objects (Selected Weapon)")]
    public GameObject m4Weapon;
    public GameObject knifeWeapon;

    private WeaponInventory inventory;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        inventory = player.GetComponent<WeaponInventory>();

        flashLightON.SetActive(false);
        flashLightOFF.SetActive(true);

        m4Icon.SetActive(false);
        knifeIcon.SetActive(false);
        handIcon.SetActive(true);
    }

    void Update()
    {
        UpdateFlashlightHUD();
        UpdateWeaponHUD();
    }

    void UpdateFlashlightHUD()
    {
        if (flashLightOB == null) return;

        bool isOn = flashLightOB.activeInHierarchy;
        flashLightON.SetActive(isOn);
        flashLightOFF.SetActive(!isOn);
    }

    void UpdateWeaponHUD()
    {
        // If M4 is selected
        if (m4Weapon.activeInHierarchy)
        {
            m4Icon.SetActive(true);
            knifeIcon.SetActive(false);
            handIcon.SetActive(false);
            return;
        }

        // If Knife is selected
        if (knifeWeapon.activeInHierarchy)
        {
            m4Icon.SetActive(false);
            knifeIcon.SetActive(true);
            handIcon.SetActive(false);
            return;
        }

        // Hands (no weapon selected)
        m4Icon.SetActive(false);
        knifeIcon.SetActive(false);
        handIcon.SetActive(true);
    }
}
