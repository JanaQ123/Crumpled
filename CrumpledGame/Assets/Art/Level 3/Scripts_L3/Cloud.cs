using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    public float targetAlpha = 0.2f;
    SpriteRenderer sr;
    float currentAlpha = 1f;
    bool fading = false;
    //[SerializeField] Player_L3 player;
    //public float playerPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //player = GetComponent<Player_L3>();
    }

    void Update()
    {
        //playerPos = player.playerZ;
        if(!fading)
            return;
        else if (fading && currentAlpha > targetAlpha)
        {
            //if (playerPos == transform.position.z)
            //{
                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
                Color current_color = sr.color;
                current_color.a = currentAlpha;
                sr.color = current_color; 
            //}
        }
    }

    public void Fade()
    {
        //print("Fade called");
        fading = true;
    }

    public void Despawn()
    {
        //print("Despawn called");
        gameObject.SetActive(false);
    }
}
