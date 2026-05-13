using UnityEngine;

public class L4SnakeDetectPlayerCollider : MonoBehaviour
{
    L4Snake snake;
    private void Start()
    {
        {
            snake= GetComponentInParent<L4Snake>(); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            snake.enteredSnake = true;
            print("player enetred");
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            snake.enteredSnake = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            snake.enteredSnake = false;
        }
    }
}
