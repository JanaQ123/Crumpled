using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    PlayableDirector timeline;
    float checkpoint = 36f;
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
}
