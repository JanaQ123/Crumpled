using UnityEngine;

public class L3_TriggerCloud : MonoBehaviour
{   
    void OnTriggerEnter(Collider other)
    {
        //if other is not a cloud, ignore
        if(!other.CompareTag("Cloud"))
        {
            return;
        }
        //if other is a cloud, fade in
        L3_Cloud cloud = other.GetComponent<L3_Cloud>();
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
        L3_Cloud cloud = other.GetComponent<L3_Cloud>();
        if(cloud != null)
        {
            cloud.FadeOut();
        }
    }
}
