
using UnityEngine;
public class LV1_PlayerParentFollow : MonoBehaviour

{

    public GameObject rollingPlayer;

    public GameObject normalPlayer;
    public GameObject playerShadow;


    private Vector3 offset;



    void Start()

    {

        offset = this.transform.position - normalPlayer.transform.position;

    }

    public void SetRollingMode(bool rolling)

    {
        rollingPlayer.SetActive(rolling);
        normalPlayer.SetActive(!rolling);
        playerShadow.SetActive(!rolling);

        if (rolling)

        {

            
            //rollingPlayer.GetComponent<LV1_PlayerController>().StartRolling();

        }

        else

        {

            // entering follow mode

            this.transform.position = new Vector3(4.6f,0.27f,0);
            normalPlayer.GetComponent<LV1_PlayerController>().StopRolling();



        }

    }

}