using UnityEngine;

public class TriggerCloud : MonoBehaviour
{   
    void OnTriggerEnter(Collider other)
    {
        //if other is not a cloud, ignore
        if(!other.CompareTag("Cloud"))
        {
            return;
        }
        //if other is a cloud, fade in
        Cloud cloud = other.GetComponent<Cloud>();
        if(cloud != null)
        {
            cloud.FadeIn();
        }
    }
    void OnTriggerExit(Collider other)
    {
        //if other is not a cloud, ignore
        if(!other.CompareTag("Cloud"))
        {
            return;
        }
        //if other is a cloud, fade out
        Cloud cloud = other.GetComponent<Cloud>();
        if(cloud != null)
        {
            cloud.FadeOut();
        }
    }
}
