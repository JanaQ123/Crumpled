using UnityEngine;

public class L3_Bird2Controller : MonoBehaviour
{
    public bool startMoving = false;
    float speed = 10;
    float scaleDuration = 2f;
    float upDuration = 1f;
    float timeElapsed = 0f;
    Vector3 startScale = Vector3.zero;
    Vector3 startYPos = Vector3.zero;
    Vector3 targetY = new Vector3(0, 3f, 0);
    Vector3 targetScale = new Vector3(2.75f, 2.75f, 2.75f);
    public L3_TimelineController timelineController;
    void Update()
    {
        if (startMoving)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (timeElapsed < scaleDuration)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / scaleDuration;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            }
            if (timelineController.timeline.time > 19f) 
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (timeElapsed < upDuration)
                {
                    timeElapsed += Time.deltaTime;
                    float y = timeElapsed / upDuration;
                    transform.localPosition = Vector3.Lerp(startYPos, targetY, y);
                }
            }
        }
    }
}
