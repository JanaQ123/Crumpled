using UnityEngine;

public class L2Collectible : MonoBehaviour
{
    public GameObject letterCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Show", 0.5f);
            gameObject.SetActive(false);
            
        }
    }
    void Show()
    {
        letterCanvas.SetActive(true);
    }
}
