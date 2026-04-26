using UnityEngine;

public class LV1_MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource suburbMusic;
    [SerializeField] private AudioSource marketMusic;

    void Start()
    {
        PlaySuburb(); // or whatever plays first
    }

    public void PlaySuburb()
    {
        marketMusic.Stop();
        suburbMusic.Play();
    }

    public void PlayMarket()
    {
        suburbMusic.Stop();
        marketMusic.Play();
    }
}