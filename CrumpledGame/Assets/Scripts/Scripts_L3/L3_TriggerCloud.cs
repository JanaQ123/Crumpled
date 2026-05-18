using UnityEngine;

public class L3_TriggerCloud : MonoBehaviour
{   
    public bool StartFadeIn;
    void OnTriggerEnter(Collider other)
    {
        if (StartFadeIn)
        {
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
        //if other is not a cloud, ignore
        
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
