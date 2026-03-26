using UnityEngine;

public class L2StickyPlatform : MonoBehaviour
{
    public float slowMultiplier = 0.5f; 
    public float duration = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            L2PlayerMovement player = collision.gameObject.GetComponent<L2PlayerMovement>();

            if (player != null)
            {
                player.ApplySlow(slowMultiplier, duration);
            }
        }
    }
}
