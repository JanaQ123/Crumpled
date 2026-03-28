using UnityEngine;

public class Cloud : MonoBehaviour
{
    float fadeSpeed = 0.3f;
    float targetAlpha;
    SpriteRenderer sr;
    float currentAlpha = 0f;
    bool fade = false; //so they dont all fade at the same time
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0f;
        sr.color = c;
    }
    void Update()
    {
        if (!fade)
        {
            return;
        }
        //fade in/out depending on current & alpha values
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
        Color current_color = sr.color;
        current_color.a = currentAlpha;
        sr.color = current_color;
    }
    public void FadeIn()
    {
        //clouds fade in
        fade = true;
        targetAlpha = 1f;
        fadeSpeed = 0.3f;
    }
    public void FadeOut()
    {
        //clouds fade out
        fade = true;
        targetAlpha = 0f;
        fadeSpeed = 0.3f;
    }
}