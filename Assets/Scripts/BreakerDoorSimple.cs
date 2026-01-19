using UnityEngine;

public class BreakerDoorSimple : MonoBehaviour
{
    public FuseInsert fuseScript;
    public GameObject door;
    public AudioSource breakerSound;
    public AudioSource doorSound;

    bool breakerOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!fuseScript.fuseInserted) return;

            if (!breakerOn)
            {
                breakerOn = true;

                breakerSound.Play();
                doorSound.Play();

                door.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
