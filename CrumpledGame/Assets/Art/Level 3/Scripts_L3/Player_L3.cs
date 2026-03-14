using UnityEngine;

public class Player_L3 : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
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

    void OnTriggerExit2D(Collider2D other)
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
