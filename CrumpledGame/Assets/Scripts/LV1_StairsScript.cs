using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
public class LV1_StairsScript : MonoBehaviour
{
    public CinemachineCamera followCamera;   
    public CinemachineCamera stationaryCamera;
    [SerializeField] GameObject playerParent;
    [SerializeField] GameObject cameraTarget;

    GameObject player;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            player=other.gameObject;
            LV1_PlayerController pc = player.GetComponent<LV1_PlayerController>();
            pc.setStairControl();
            pc.targetPos = player.transform.position;
            pc.isSwitching = false;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerParent.transform.position = new Vector3(playerParent.transform.position.x, -7.73f, 0f);
            cameraTarget.GetComponent<LV1_CameraTarget>().ChangeY();
            player.GetComponent<LV1_PlayerController>().setStairControl();
        }
    }
}
