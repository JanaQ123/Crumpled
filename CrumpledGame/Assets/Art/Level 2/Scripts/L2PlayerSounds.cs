using UnityEngine;

public class L2PlayerSounds : MonoBehaviour
{
   
    [SerializeField] private AudioClip[] hitSounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Auto-adds Audio Source if you forgot to add it manually
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void HitSound()
    {
        //if (hitSounds.Length == 0) return;

        //int randomIndex = Random.Range(0, hitSounds.Length);
        //audioSource.PlayOneShot(hitSounds[randomIndex]);
    }
    
}
