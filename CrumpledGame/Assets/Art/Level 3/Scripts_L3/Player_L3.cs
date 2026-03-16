using UnityEngine;

public class Player_L3 : MonoBehaviour
{
    //public float playerZ;
    void Start()
    {
        
    }

    void Update()
    {
       //playerZ = transform.position.z; 
    }

    void OnTriggerEnter(Collider other)
    {
        print("player collided with " + other.name);
        if(!other.CompareTag("Cloud"))
        {
            return;
        }
        Cloud cloud = other.GetComponent<Cloud>();
        if(cloud != null)
        {
            cloud.Fade();
        }
    }
}
