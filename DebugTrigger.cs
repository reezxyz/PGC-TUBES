using UnityEngine;

public class DebugTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ENTER trigger: " + gameObject.name);
        }
    }
}
