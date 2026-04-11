
using UnityEngine;
public class LV1_PlayerParentFollow : MonoBehaviour

{

    public GameObject rollingPlayer;

    public GameObject normalPlayer;

    private Vector3 offset;



    void Start()

    {

        offset = this.transform.position - normalPlayer.transform.position;

    }

    public void SetRollingMode(bool rolling)

    {
        rollingPlayer.SetActive(rolling);
        normalPlayer.SetActive(!rolling);

        if (rolling)

        {

            
            rollingPlayer.GetComponent<LV1_PlayerController>().StartRolling();

        }

        else

        {

            // entering follow mode

            this.transform.position = new Vector3(2.3f,0.27f,0);
            normalPlayer.GetComponent<LV1_PlayerController>().StopRolling();



        }

    }

}