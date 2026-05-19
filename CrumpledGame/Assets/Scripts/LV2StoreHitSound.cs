using UnityEngine;

public class LV2StoreHitSound : MonoBehaviour
{

    [SerializeField] private AudioClip[] hitSounds;
    public void HitSound(AudioSource audioSource)
    {
        int randomIndex = Random.Range(0, hitSounds.Length);
        audioSource.PlayOneShot(hitSounds[randomIndex]);
    }
}
