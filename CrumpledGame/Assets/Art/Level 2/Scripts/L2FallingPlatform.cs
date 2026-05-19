using UnityEngine;
using System.Collections;

public class L2FallingPlatform : MonoBehaviour
{
    public float shakeDelay = 0.5f;
    public float shakeDuration = 0.5f;
    public float respawnDelay = 3.5f;
    public float shakeMagnitude = 0.05f;
    public float randomSpinForce = 80f;

    public GameObject platformPrefab;

    float shakeTimer = 0f;
    bool playerOnPlatform = false;
    bool isShaking = false;
    bool isFalling = false;
    bool hasSpawned = false;

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

    void Update()
    {
        if (isFalling || isShaking) return;

        if (playerOnPlatform)
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer >= shakeDelay)
                StartShaking();
        }
        else
        {
            shakeTimer = 0f;
        }
    }

    void StartShaking()
    {
        isShaking = true;
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
            Fall();
        else
            shakeTimer = 0f;
    }

    void Fall()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.angularVelocity = Random.Range(-randomSpinForce, randomSpinForce);

        Invoke("SpawnNewPlatform", respawnDelay);
    }

    void SpawnNewPlatform()
    {
        if (hasSpawned) return;
        hasSpawned = true;

        // Spawn a new platform — it handles its own drop logic
        Vector3 spawnPos = originalPosition + new Vector3(0, 6f, 0);
        GameObject newGO = Instantiate(platformPrefab, spawnPos, originalRotation);

        // Give it the drop target
        L2FallingPlatformSpawned spawned = newGO.GetComponent<L2FallingPlatformSpawned>();
        if (spawned != null) spawned.Init(originalPosition, originalRotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnPlatform = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnPlatform = false;
    }
}