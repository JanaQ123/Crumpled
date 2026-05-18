using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class L2KnockablePlatform : MonoBehaviour
{
    public int hitsToFall = 3;
    private int currentHits = 0;

    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;
    float shakeTimer;

    public float fallRotation = 90f;
    public float fallSpeed = 3f;

    bool isFalling = false;
    bool isShaking = false;
    public float pushValue = -2;

    public Transform visual;

    Vector3 originalPos;
    Quaternion originalRotation;
    Quaternion targetRotation;

    void Start()
    {
        originalRotation = visual.localRotation;
        originalPos = visual.localPosition;
        targetRotation = visual.rotation;
    }

    void Update()
    {
        HandleShake();
        HandleFall();

    }

    void HandleShake()
    {
        if (!isShaking) return;

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            float intensity = shakeAmount * currentHits;

            float x = UnityEngine.Random.Range(-intensity, intensity);
            float r = UnityEngine.Random.Range(-5f * currentHits, 5f * currentHits);

            visual.localPosition = originalPos + new Vector3(x, 0, 0);
            visual.localRotation = Quaternion.Euler(0, 0, r);
        }
        else
        {
            isShaking = false;

            visual.localPosition = originalPos;
            visual.localRotation = originalRotation;
        }
    }

    void HandleFall()
    {
        if (!isFalling) return;
        print("fall sideways");
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,Time.deltaTime * fallSpeed);
    }

    public void Hit()
    {
        if (isFalling)
        {
            transform.position += new Vector3(pushValue, 0, 0);
            return;
        }

        currentHits++;

        isShaking = true;
        shakeTimer = shakeDuration;


        if (currentHits >= hitsToFall)
        {
            isFalling = true;
            print("falling");
            targetRotation = Quaternion.Euler(0, 0, fallRotation);
        }
    }

  
}

