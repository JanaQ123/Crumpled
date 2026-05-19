using TMPro;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public static int collectedLetters = -1;
    public static LetterUI[] letterUIs;
    public LetterUI[] letterData;
    public TMP_Text noSelectionText;
    public TMP_Text collectedLettersText;
    static LetterManager instance;
    public GameObject bookMenu;
    void Start()
    {
        letterUIs = letterData;
        instance = this;
    }
    public static void CollectLetter()
    {
        collectedLetters++;
        letterUIs[collectedLetters].collected = true;
        instance.ChangeTotal();
    }
    public void SwitchSelection(int index)
    {
        if(noSelectionText.gameObject.activeInHierarchy)
        {
            noSelectionText.gameObject.SetActive(false);
        }

        for(int i = 0; i <= collectedLetters; i++)
        {
            if(letterUIs[i].collected && letterUIs[i].selected)
            {
               letterUIs[i].selectedObject.SetActive(false);
               if(letterUIs[i].letterView != null)
                    letterUIs[i].letterView.SetActive(false);
               letterUIs[i].selected = false;
            }
        }
        letterUIs[index].SelectLetter();    
    }
    void ChangeTotal()
    {
        collectedLettersText.text = (collectedLetters + 1) + "/5 Letters Collected";
    }

    public void ShowBook()
    {
        bookMenu.SetActive(true);
    }
    public void HideBook()
    {
        bookMenu.SetActive(false);
    }
}