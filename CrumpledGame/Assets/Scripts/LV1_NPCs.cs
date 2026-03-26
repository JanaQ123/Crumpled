using UnityEngine;
using UnityEngine.Identifiers;

public class LV1_NPCs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("entered");
        if (collision.gameObject.tag == "Player")
        { collision.gameObject.GetComponent<LV1_PlayerController>().Kick(); }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { collision.gameObject.GetComponent<LV1_PlayerController>().Kick(); }
    }


}
