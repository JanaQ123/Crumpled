using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour
{
    private AudioSource audioSource;
    LV2StoreHitSound lV2StoreHitSound;

    void Start()
    {
        lV2StoreHitSound = GameObject.Find("SoundStorage").GetComponent<LV2StoreHitSound>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.priority = 148;
        audioSource.volume = 1f;
        audioSource.pitch = 1f;
        audioSource.panStereo = 0f;
        audioSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        audioSource.reverbZoneMix = 1f;


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                lV2StoreHitSound.HitSound(audioSource);

            }
        }
    }
}