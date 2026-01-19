using UnityEngine;

public class FuseInsert : MonoBehaviour
{
    public bool fuseInserted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuse"))
        {
            fuseInserted = true;
            Destroy(other.gameObject);
            Debug.Log("Fuse Inserted");
        }
    }
}
