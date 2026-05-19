using UnityEngine;
using UnityEngine.Playables;

public class L4CSTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject CSCamera;
    public GameObject player;

    bool hasPlayed = false;

    private void Start()
    {
        timeline.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        print("hellooo???");
        player.SetActive(false);
        //if (hasPlayed)
        //    return;

        if (other.CompareTag("Player"))
        {
            hasPlayed = true;
            print("hi");
            //CSCamera.SetActive(true);
            timeline.Play();
        }
    }
}

