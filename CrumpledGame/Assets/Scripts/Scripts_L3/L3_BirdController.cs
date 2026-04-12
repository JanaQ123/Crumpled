using UnityEngine;

public class L3_BirdController : MonoBehaviour
{
    public bool startMoving = false;
    float speed = 10;
    public float scaleDuration = 2f;
    float timeElapsed = 0f;

    Vector3 startScale = Vector3.zero;
    Vector3 targetScale = new Vector3(2.75f, 2.75f, 2.75f);
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
        }
    }
}
