using UnityEngine;

public class L2EndFallTrigger : MonoBehaviour
{
    bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        L2PlayerMovement player = other.GetComponent<L2PlayerMovement>();
        if (player == null) return;

        // Only fire if the fall scene has actually started
        if (!player.fallSceneStarted) return;

        hasTriggered = true;
        player.EndFallScene();


        gameObject.SetActive(false);
    }
}
