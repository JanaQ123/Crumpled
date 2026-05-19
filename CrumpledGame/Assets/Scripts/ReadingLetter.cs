using UnityEngine;
using System;
using System.Collections;


public class ReadingLetter : MonoBehaviour
{
    [SerializeField] GameObject LetterPanel;
    AudioSource paperOpen;
    Animator animator;
    void Start()
    {
        animator = LetterPanel.GetComponentInChildren<Animator>();
        LetterPanel.SetActive(false);
        paperOpen = GetComponent<AudioSource>();
        paperOpen.ignoreListenerPause = true;


    }

    public void ShowLetter()
    {
        LetterPanel.SetActive(true);
        //static int ++
        paperOpen.Play();
        Time.timeScale = 0;
        AudioListener.pause = true;


    }
    public void HideLetter()
    {
        print("Im tryna hiiiiideeeeee");

        LetterPanel.SetActive(false);
        paperOpen.Play();
        Time.timeScale =1 ;
        AudioListener.pause = false;



    }

    //IEnumerator PauseAfterFrame()
    //{
    //    paperOpen.Play();
    //    yield return null; // wait 1 frame
    //    Time.timeScale = 0;
    //}
}
