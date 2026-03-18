using UnityEngine;

public class Cloud : MonoBehaviour
{
    float fadeSpeed = 0.2f;
    float targetAlpha = 1f;
    SpriteRenderer sr;
    float currentAlpha = 0.2f;
    bool fadeIn = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!fadeIn)
            return;
        else if (fadeIn && (currentAlpha < targetAlpha))
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            Color current_color = sr.color;
            current_color.a = currentAlpha;
            sr.color = current_color; 
        }
    }

    public void Fade()
    {
        //tells clouds to fade
        fadeIn = true;
    }

    public void Despawn()
    {
        //destroys clouds
        Destroy(gameObject);
    }
}
