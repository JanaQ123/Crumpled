using UnityEngine;

public class L2EndTrigger : MonoBehaviour
{
    public GameObject[] fallingObjects;
    bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;


        foreach (GameObject obj in fallingObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        L2PlayerMovement player = other.GetComponent<L2PlayerMovement>();
        if (player != null)
        {
            print("enteredFallScene");
            player.StartFallScene();
        }

        gameObject.SetActive(false);
    }
}
