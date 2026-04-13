using UnityEngine;

public class L2IntroTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        L2PlayerMovement player = other.GetComponent<L2PlayerMovement>();
        if (player != null)
        {
            print("triggered");
            player.FinishIntro();
            gameObject.SetActive(false); // disable trigger so it never fires again
        }
    }
}
