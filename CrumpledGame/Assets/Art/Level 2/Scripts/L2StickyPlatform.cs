using UnityEngine;

public class L2StickyPlatform : MonoBehaviour
{
    public float slowMultiplier = 0.05f; 
    public float duration = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            L2PlayerMovement player = collision.gameObject.GetComponent<L2PlayerMovement>();

            if (player != null)
            {
                print("I entered slow");
                player.ApplySlow(slowMultiplier);
            }
        }
    }
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        L2PlayerMovement player = collision.gameObject.GetComponent<L2PlayerMovement>();

    //        if (player != null)
    //        {
    //            player.RemoveSlow();
    //        }
    //    }
    //}
}
