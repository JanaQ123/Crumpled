using UnityEngine;

public class LV1_CameraTarget : MonoBehaviour
{
    [SerializeField] GameObject player;


    float y;
    void Start()
    {
        y=this.transform.localPosition.y;
    }
    void Update()
    {
        if (player.GetComponent<LV1_PlayerController>().getStairStatus())
        {
            this.transform.localPosition = new Vector3(180, y, 0); //to prevent camera flying
            
        }
        else
        {
            this.transform.localPosition = new Vector3(player.transform.localPosition.x, y, 0);
        }
    }
    public void ChangeY()
    {
        y = -4.6f;
    }
    



}
