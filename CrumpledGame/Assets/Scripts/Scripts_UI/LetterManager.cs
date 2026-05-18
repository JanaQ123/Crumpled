using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public static bool[] collectedLetters = new bool[5];

    public static int selectedLetter = -1;

    public static void CollectLetter(int index)
    {
        collectedLetters[index] = true;
    }
}