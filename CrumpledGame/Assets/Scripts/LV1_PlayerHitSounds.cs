using UnityEngine;

public class LV1_PlayerHitSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] hitSounds; // Assign your 21 sounds in Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("AudioSource found: " + audioSource, gameObject);
    }

    public void OnHitByNPC()
    {
        Debug.Log("OnHitByNPC called on: " + gameObject.name);
        if (audioSource.isPlaying) return;
        if (hitSounds.Length == 0)
        {
            Debug.Log("Hit sounds array is empty!");
            return;
        }

        int randomIndex = Random.Range(0, hitSounds.Length);
        audioSource.PlayOneShot(hitSounds[randomIndex]);
    }
}