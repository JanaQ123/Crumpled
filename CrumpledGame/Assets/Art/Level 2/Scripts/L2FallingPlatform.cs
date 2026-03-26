using UnityEngine;
using System.Collections;

public class L2FallingPlatform : MonoBehaviour
{
    public float delayBeforeShake = 1.5f;
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.1f;

    bool isTriggered = false;
    Vector3 originalPosition;
    Rigidbody2D rb;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    public void ActivatePlatform()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(FallSequence());
        }
    }

    IEnumerator FallSequence()
    {
         yield return new WaitForSeconds(delayBeforeShake);
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount, shakeAmount);

            transform.position = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;

        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
