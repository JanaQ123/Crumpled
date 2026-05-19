using UnityEngine;

public class L4PlayerLeftHill : MonoBehaviour
{
    public L4Bird bird;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bird.startFollowing = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bird.startFollowing = true;

        }
    }
}
