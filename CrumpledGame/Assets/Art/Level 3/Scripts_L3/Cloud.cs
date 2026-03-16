using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float fadeSpeed = 0.1f;
    public float targetAlpha = 0.2f;
    SpriteRenderer sr;
    float currentAlpha = 1f;
    bool fading = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!fading)
            return;
        else if (fading && currentAlpha > targetAlpha)
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
        fading = true;
    }

    public void Despawn()
    {
        //hides clouds
        gameObject.SetActive(false);
    }
}
