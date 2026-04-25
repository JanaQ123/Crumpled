using UnityEngine;
using UnityEngine.Playables;

public class L3_TimelineController : MonoBehaviour
{
    public PlayableDirector timeline;
    float checkpoint = 36f;
    [SerializeField] GameObject player;
    [SerializeField] Animator playerAnim;
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(1.4f);
    }
    public void StopTimeline()
    {
        timeline.enabled = false;
        player.GetComponent<L3_Player>().isDead = true;
        playerAnim.Play("player_died");
    }
    public void RestartAtCheckPoint()
    {
        player.GetComponent<L3_Player>().isDead = false;
        playerAnim.Play("flying");
        timeline.enabled = true;
        timeline.time = checkpoint;
        timeline.Play();
    }
}
