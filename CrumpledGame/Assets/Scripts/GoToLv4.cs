using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLv4 : MonoBehaviour
{
    [SerializeField] GameObject panel;
    public void NextLevel()
    {
        panel.SetActive(true);
        Invoke("Switch", 2f);
    }
    void Switch()
    {
        SceneManager.LoadScene("Level 4");

    }
}
