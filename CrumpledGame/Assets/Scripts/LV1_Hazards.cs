using UnityEngine;

public class LV1_Hazards : MonoBehaviour
{
    [SerializeField] int identifier;
    GameObject player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            if(identifier == 0)
            {
                player.GetComponent<LV1_PlayerController>().StartSewerCover();
            }
            if (identifier == 1)
            {
                player.GetComponent<LV1_PlayerController>().StartGum();
                if (this.gameObject.name == "EndingGum")
                {
                    player.GetComponent<LV1_PlayerController>().inEndingGum = true;

                }
            }





        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            if (identifier == 0)
            {
                player.GetComponent<LV1_PlayerController>().EndSewerCover();
            }
          
        }
    }

}
