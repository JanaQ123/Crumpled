using UnityEngine;

public class LV1_NPC : MonoBehaviour
{
    float speed = 9f;
    bool isWalking = true;
    Animator anim;
   
    void Update()
    {
        //if (isWalking)
        print("script is being called");
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

}
