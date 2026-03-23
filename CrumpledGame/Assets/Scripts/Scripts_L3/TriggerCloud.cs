using UnityEngine;

public class TriggerCloud : MonoBehaviour
{   
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cloud"))
        {
            Cloud cloud = other.GetComponent<Cloud>();
            if(cloud != null)
            {
                cloud.FadeIn();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //hides clouds after player passes through them
        if(!other.CompareTag("Cloud"))
        {
            return;
        }
        Cloud cloud = other.GetComponent<Cloud>();
        if(cloud != null)
        {
            //cloud.Despawn();
            cloud.FadeOut();
        }
    }
}
