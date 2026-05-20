using UnityEngine;

public class ScreenSettings : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }
}