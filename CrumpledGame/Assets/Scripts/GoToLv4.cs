using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLv4 : MonoBehaviour
{
    public void NextLevel()
    {
        Invoke("Switch", 2f);
    }
    void Switch()
    {
        SceneManager.LoadScene("Level 4");

    }
}
