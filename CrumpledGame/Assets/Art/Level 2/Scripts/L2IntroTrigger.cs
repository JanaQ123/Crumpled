using UnityEngine;

public class L2IntroTrigger : MonoBehaviour
{
    public GameObject canvas;

    L2PlayerMovement player;
    //AudioSource audioS;
    public AudioSource ST;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<L2PlayerMovement>();

        if (player != null)
        {
            player.FinishIntro();

            player.canMove = false;

            gameObject.SetActive(false);

            Invoke(nameof(ShowCanvas), 1f);
        }
    }

    void ShowCanvas()
    {
        canvas.SetActive(true);

        AudioListener.pause = true;
    }

    public void StartGame()
    {
        if (player == null)
            print("i can't find the player :(");

        canvas.SetActive(false);
        ST.Play();
        AudioListener.pause = false;

        player.canMove = true;
        
        
    }
}