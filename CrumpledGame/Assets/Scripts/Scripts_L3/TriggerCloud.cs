using UnityEngine;

public class TriggerCloud : MonoBehaviour
{
    Animator airplaneAnimation;
    
    void Start()
    {
        airplaneAnimation = GameObject.Find("airplane").GetComponent<Animator>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cloud"))
        {
            Cloud cloud = other.GetComponent<Cloud>();
            if(cloud != null)
            {
                cloud.Fade();
            }
        }
        else if (other.CompareTag("Airplane"))
        {
            //triggers airplane animation
            airplaneAnimation.SetTrigger("TakeOff");
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
            cloud.Despawn();
    }
}
