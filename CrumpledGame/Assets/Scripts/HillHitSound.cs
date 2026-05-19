using UnityEngine;

public class HillHitSound : MonoBehaviour
{
    public LV1_PlayerHitSounds hitParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitParent.OnHitByNPC();
        }
    }
}
