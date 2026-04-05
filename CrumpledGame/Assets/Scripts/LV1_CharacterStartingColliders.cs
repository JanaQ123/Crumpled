using UnityEngine;

public class LV1_CharacterStartingColliders : MonoBehaviour
{
    [SerializeField] GameObject [] Characters;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Characters.Length; i++)
            {
                Characters[i].gameObject.GetComponentInChildren<LV1_NPCs>().StartWalking();
            }
        }
    }
}