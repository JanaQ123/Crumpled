using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public GameObject settingsMenu;
    public L3_TriggerCloud triggerCloud;
    public L3_TimelineController timelineController;
    public L3_Bird1Controller bird1Controller;
    public GameObject volumeSprite;
    public GameObject muteSprite;
    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    public void Resume()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
    public void Mute()
    {
        volumeSprite.SetActive(false);
        muteSprite.SetActive(true);
        AudioListener.volume = 0f;
    }
    public void AddVolume()
    {
        volumeSprite.SetActive(true);
        muteSprite.SetActive(false);
        AudioListener.volume = 1f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
