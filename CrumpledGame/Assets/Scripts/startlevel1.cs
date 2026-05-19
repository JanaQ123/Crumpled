using UnityEngine;
using UnityEngine.SceneManagement;

public class startlevel1 : MonoBehaviour
{
   public void SwitchLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
}
