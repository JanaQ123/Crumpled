using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
   public void Switch()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
