using UnityEngine;

public class L3_TornadoAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundEffect;

    public void PlaySound()
    {
        audioSource.PlayOneShot(soundEffect);
    }
}
