using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    PlayableDirector timeline;
    float checkpoint = 36f;
    [SerializeField] GameObject player;
    [SerializeField] Animator playerAnim;
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }
    public void StopTimeline()
    {
        timeline.enabled = false;
        player.GetComponent<Player_L3>().isDead = true;
        playerAnim.Play("player_died");
    }
    public void RestartAtCheckPoint()
    {
        player.GetComponent<Player_L3>().isDead = false;
        playerAnim.Play("flying");
        timeline.enabled = true;
        timeline.time = checkpoint;
        timeline.Play();
    }
}
