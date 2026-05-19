using UnityEngine;
using UnityEngine.Playables;

public class TimelineSequencer : MonoBehaviour
{
   
        public PlayableDirector firstTimeline;
        public PlayableDirector secondTimeline;
        public GameObject secondPlayer;
        public GameObject FirstPlayer;


    void Start()
        {
            //firstTimeline.stopped += OnFirstTimelineFinished;
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
