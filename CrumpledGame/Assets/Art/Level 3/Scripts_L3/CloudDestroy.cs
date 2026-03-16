using UnityEngine;

public class CloudDestroy : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Cloud"))
        {
            return;
        }
        Cloud cloud = other.GetComponent<Cloud>();
        if(cloud != null)
            cloud.Despawn();
    }
}
