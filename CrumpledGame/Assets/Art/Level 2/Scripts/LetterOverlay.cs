using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class LetterOverlay : MonoBehaviour
{
    public static LetterOverlay Instance;
    public GameObject overlayPanel;
    bool dismissed = false;

    public bool IsReadingLetter { get; private set; }

    void Awake() => Instance = this;

    void Update()
    {
        if (!IsReadingLetter) return;


        if (dismissed)
        {
            overlayPanel.SetActive(false);
            IsReadingLetter = false;
        }
    }
    public void OnClick()
    {
        overlayPanel.SetActive(false);
        IsReadingLetter = false;
    }
    public void ShowLetter()
    {
        overlayPanel.SetActive(true);
        IsReadingLetter = true;
    }
}