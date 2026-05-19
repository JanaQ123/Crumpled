using System;
using UnityEngine;

public class CollectLetter : MonoBehaviour
{
    [SerializeField] ReadingLetter letterOverlay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            letterOverlay.ShowLetter();
            this.gameObject.SetActive(false);
        }
    }
}
