using UnityEngine;
using UnityEngine.InputSystem; // <- New Input System

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // New Input System check
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            BedInteract bed = hit.collider.GetComponent<BedInteract>();
            if (bed != null)
            {
                bed.Sleep();
            }
        }
    }
}
