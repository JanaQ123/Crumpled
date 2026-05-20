using UnityEngine;

public class LetterUI : MonoBehaviour
{
    public int letterIndex;
    public GameObject unknownObject;
    public GameObject foundObject;
    public GameObject selectedObject;
    public bool collected;
    public bool selected;
    public GameObject letterView;
    public LetterManager letterManager;
    void Start()
    {
        if (LetterManager.collectedLetters >= letterIndex)
        {
            collected = true;
        }
    }
    void Update()
    {
        RefreshState();
    }
    void RefreshState()
    {
        // NOT FOUND
        if (!collected)
        {
            unknownObject.SetActive(true);

            if(letterView != null)
                letterView.SetActive(false);

            return;
        }
        // FOUND BUT NOT SELECTED
        else
        {
            foundObject.SetActive(true);
        }
    }
    public void IAmSelected()
    {
        letterManager.SwitchSelection(letterIndex);
    }
    public void SelectLetter()
    {
        selectedObject.SetActive(true);

        if(letterView != null)
                letterView.SetActive(true);
        selected = true;
    }
}