using UnityEngine;

public class PlayCandySound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            print(gameObject.name);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
