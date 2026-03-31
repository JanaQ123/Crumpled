using UnityEngine;
using System.Collections;

public class L2FallingPlatform : MonoBehaviour
{
    public float shakeDelay = 1f;
    public float shakeDuration = 0.5f;
    public float respawnDelay = 3.5f;
    public float shakeMagnitude = 0.05f;

    float shakeDurationTimer = 0f;
    float shakeTimer = 0f;

    bool playerOnPlatform = false;
    bool isShaking = false;
    bool isFalling = false;
    //bool isTriggered = false;

    Vector3 originalPosition;
    Quaternion originalRotation;
    Rigidbody2D rb;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    private void Update()
    {
        if (isFalling || isShaking) return;

        if (playerOnPlatform)
        {
            shakeTimer += Time.deltaTime;

            if (shakeTimer >= shakeDelay)
            {
                StartShaking();
            }
        }
        else
        {
            shakeTimer = 0f;
        }
    }

    void StartShaking()
    {
        isShaking = true;
        shakeDurationTimer = 0f;
        InvokeRepeating("ShakePlatform", 0f, 0.02f);
        Invoke("CheckIfShouldFall", shakeDuration);
    }
    void ShakePlatform()
    {
        transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
    }
    void CheckIfShouldFall()
    {
        CancelInvoke("ShakePlatform");
        transform.position = originalPosition;
        isShaking = false;

        if (playerOnPlatform)
        {
            Fall();
        }
        else
        {
            // Player escaped, reset everything
            shakeTimer = 0f;
        }
    }
    void Fall()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("Respawn", respawnDelay);
    }
    void Respawn()
    {
        // Reset state
        isFalling = false;
        playerOnPlatform = false;
        shakeTimer = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}

