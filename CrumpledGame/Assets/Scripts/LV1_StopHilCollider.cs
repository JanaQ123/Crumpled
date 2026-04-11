using UnityEngine;

public class LV1_StopHillCollider : MonoBehaviour

{
    [SerializeField] GameObject parent;
    void Start()

    {

        parent.GetComponent<LV1_PlayerParentFollow>().SetRollingMode(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.gameObject.tag == "Player")

        {
            parent.GetComponent<LV1_PlayerParentFollow>().SetRollingMode(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<LV1_PlayerController>().StartLanes();
        }



    }

}