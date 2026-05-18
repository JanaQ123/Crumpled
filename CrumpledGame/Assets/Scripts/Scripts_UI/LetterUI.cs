using UnityEngine;

public class LetterUI : MonoBehaviour
{
    public int letterIndex;

    public GameObject unknownObject;
    public GameObject foundObject;
    public GameObject selectedObject;

    // Right page
    public GameObject letterView;

    void Update()
    {
        RefreshState();
    }

    void RefreshState()
    {
        bool collected = LetterManager.collectedLetters[letterIndex];
        bool selected = LetterManager.selectedLetter == letterIndex;

        // NOT FOUND
        if (!collected)
        {
            unknownObject.SetActive(true);
            //foundObject.SetActive(false);
            //selectedObject.SetActive(false);

            if(letterView != null)
                letterView.SetActive(false);

            return;
        }

        // FOUND BUT NOT SELECTED
        if (!selected)
        {
            //unknownObject.SetActive(false);
            foundObject.SetActive(true);
            //selectedObject.SetActive(false);

            if(letterView != null)
                letterView.SetActive(false);

            return;
        }

        // SELECTED
        //unknownObject.SetActive(false);
        //foundObject.SetActive(false);
        selectedObject.SetActive(true);

        if(letterView != null)
            letterView.SetActive(true);
    }

    public void SelectLetter()
    {
        if (!LetterManager.collectedLetters[letterIndex])
            return;

        LetterManager.selectedLetter = letterIndex;
    }
}