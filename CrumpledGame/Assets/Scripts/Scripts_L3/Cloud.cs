using UnityEngine;

public class Cloud : MonoBehaviour
{
    float fadeSpeed = 0.4f;
    float targetAlpha;
    SpriteRenderer sr;
    float currentAlpha = 0f;
    bool fadeIn = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!fadeIn)
        {
            targetAlpha = 0.5f;
            fadeSpeed = 0.6f;
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            Color current_color = sr.color;
            current_color.a = currentAlpha;
            sr.color = current_color;
        }
        else if (fadeIn)
        {
            targetAlpha = 1f;
            fadeSpeed = 0.1f;
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            Color current_color = sr.color;
            current_color.a = currentAlpha;
            sr.color = current_color; 
        }
    }
    public void FadeIn()
    {
        //tells clouds to fade
        fadeIn = true;
    }
    public void FadeOut()
    {
        //tells clouds to fade out
        fadeIn = false;
    }
    // public void Despawn()
    // {
    //     //destroys clouds
    //    Destroy(gameObject);
    // }
}