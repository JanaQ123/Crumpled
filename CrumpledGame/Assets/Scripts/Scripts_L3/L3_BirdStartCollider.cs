using UnityEngine;

public class L3_BirdStartCollider : MonoBehaviour
{
    public GameObject birdController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("I am moving cawk cawk");
            birdController.GetComponent<L3_BirdController>().startMoving = true;
        }
    }
}
