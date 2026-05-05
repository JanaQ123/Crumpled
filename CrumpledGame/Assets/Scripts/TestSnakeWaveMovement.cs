using UnityEngine;

public class TestSnakeWaveMovement : MonoBehaviour
{
    [Header("Segments")]
    public Transform[] segments; // Drag all snake bone segments here in order

    [Header("Wave Settings")]
    public float waveAmplitude = 1f;    // How wide the wave is
    public float waveFrequency = 2f;    // How many waves along the body
    public float waveSpeed = 3f;        // How fast the wave moves

    [Header("Forward Movement")]
    public float moveSpeed = 2f;        // How fast the snake moves forward

    void Update()
    {
        // Move the whole snake forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Apply wave to each segment
        for (int i = 0; i < segments.Length; i++)
        {
            float offset = (float)i / segments.Length; // 0 to 1 along the body
            float wave = Mathf.Sin(Time.time * waveSpeed - offset * waveFrequency * Mathf.PI * 2) * waveAmplitude;

            Vector3 pos = segments[i].localPosition;
            segments[i].localPosition = new Vector3(pos.x, wave, pos.z);
        }
    }
}