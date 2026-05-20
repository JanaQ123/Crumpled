using UnityEngine;
using UnityEngine.Playables;

public class TimelineSequencer : MonoBehaviour
{
   
        public PlayableDirector firstTimeline;
        public PlayableDirector secondTimeline;
        public GameObject secondPlayer;
        public GameObject FirstPlayer;
    public bool firstScene;

    void Start()
        {
        if (firstScene)
        {
            firstTimeline.Play();
        }
        secondPlayer.SetActive(false);  
        }

        public void OnFirstTimelineFinished(PlayableDirector director)
        {
        firstTimeline.Stop();
        FirstPlayer.SetActive(false);

        secondPlayer.SetActive(true);
        secondTimeline.Play();
        }
    
}
