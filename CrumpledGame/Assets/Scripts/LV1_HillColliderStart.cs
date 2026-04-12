using UnityEngine;

public class LV1_HillColliderStart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<LV1_StartBallHandler>().canMove = true;
            

        }
    }
}
