using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class L2KnockablePlatform : MonoBehaviour
{
    public int hitsToFall = 3;
    private int currentHits = 0;

    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;

    public float fallRotation = 90f;
    public float fallSpeed = 3f;

    bool isFalling = false;
    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    public void Hit()
    {
        if (isFalling) return;

        currentHits++;

        StartCoroutine(Shake());

        if (currentHits >= hitsToFall)
        {
            StartCoroutine(FallOver());
        }
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            transform.localPosition = originalPos + new Vector3(x, 0, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    IEnumerator FallOver()
    {
        isFalling = true;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, fallRotation);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * fallSpeed;
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }
    }
}

