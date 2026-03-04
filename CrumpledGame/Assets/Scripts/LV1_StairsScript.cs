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

            followCamera.Priority = 1;
            stationaryCamera.Priority = 2; 
            player=other.gameObject;
            LV1_PlayerController pc = player.GetComponent<LV1_PlayerController>();
            cameraTarget.transform.SetParent(null);
            pc.setStairControl();
            pc.targetPos = player.transform.position;
            pc.isSwitching = false;
            player.transform.SetParent(null);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            followCamera.Priority = 2; 
            stationaryCamera.Priority = 1;
            playerParent.transform.position = new Vector3(playerParent.transform.position.x, -7.37f, 0f);
            player.transform.SetParent(playerParent.transform);
            cameraTarget.transform.SetParent(playerParent.transform);
            player.GetComponent<LV1_PlayerController>().setStairControl();
        }
    }
}
