using UnityEngine;
using UnityEngine.Audio;

public class PlayCanSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("CanSounds").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
