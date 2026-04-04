using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    PlayableDirector timeline;
    float checkpoint = 36f;
    //[SerializeField] Player_L3 playerScript;
    //[SerializeField] Animator playerAnim;
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }
    public void RestartAtCheckPoint()
    {
        timeline.Stop();
        timeline.time = checkpoint;
        timeline.Evaluate();
        timeline.Play();
    }
    public void StopTimeline()
    {
        timeline.enabled = false;
        //playerScript.isDead = true;
        //playerAnim.Play("player_died");    
    }
}
