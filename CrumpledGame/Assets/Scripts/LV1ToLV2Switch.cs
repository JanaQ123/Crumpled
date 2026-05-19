using UnityEngine;
using UnityEngine.SceneManagement;

public class LV1ToLV2Switch : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("Level 2");
    }
}
