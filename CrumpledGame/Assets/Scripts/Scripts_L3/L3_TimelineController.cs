using UnityEngine;
using UnityEngine.Playables;

public class L3_TimelineController : MonoBehaviour
{
    public PlayableDirector timeline;
    float checkpoint = 36f;
    [SerializeField] GameObject player;
    [SerializeField] Animator playerAnim;
    public AudioSource musicSource;
    float restartAudioTime = 25.5f;
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
    public void StopForCutscene()
    {
        timeline.enabled = false;
        player.GetComponent<L3_Player>().isDead = true;
        playerAnim.Play("tornado_player");
    }
    public void RestartAtCheckPoint()
    {
        player.GetComponent<L3_Player>().isDead = false;
        playerAnim.Play("flying");
        timeline.enabled = true;
        timeline.time = checkpoint;
        musicSource.time = restartAudioTime;
        timeline.Play();
    }
}
