using UnityEngine;
using System.Collections;

public class L2FallingPlatformSpawned : MonoBehaviour
{
    Vector3 targetPosition;
    Quaternion targetRotation;
    Rigidbody2D rb;
    bool initialized = false;
    bool landed = false;

    public void Init(Vector3 landingPos, Quaternion landingRot)
    {
        targetPosition = landingPos;
        targetRotation = landingRot;
        initialized = true;

        rb = GetComponent<Rigidbody2D>();

        // Fall naturally with gravity
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 2f; // tweak for faster/slower drop
    }

    void Update()
    {
        if (!initialized || landed) return;

        // Once it reaches the target Y, snap and lock
        if (transform.position.y <= targetPosition.y)
        {
            landed = true;

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            transform.position = targetPosition;
            transform.rotation = targetRotation;

            // Swap this script for the real falling platform script
            gameObject.AddComponent<L2FallingPlatform>().enabled = true;
            Destroy(this);
        }
    }
}