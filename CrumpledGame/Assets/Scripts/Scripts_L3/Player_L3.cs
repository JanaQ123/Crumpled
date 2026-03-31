using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 9f;
    float playerPosX;
    Vector3 direction;
    Vector3 playerPos;  
    public TimelineController timelineController;
    Camera mainCam;
    float newFarClip = 100f;
    void Start()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
       PlayerMove();
    }
    void PlayerMove()
    {
        float moveAmount = direction.x * moveSpeed * Time.deltaTime;
        playerPosX = transform.localPosition.x + moveAmount;
        playerPosX = Mathf.Clamp(playerPosX, -17.2f, 17.2f);
        transform.localPosition = new Vector3(playerPosX, transform.localPosition.y, transform.localPosition.z);
    }
    public void OnMove(InputValue value)
    {
        direction = new Vector3(value.Get<Vector2>().x, 0f, 0f);
    }
    void OnCollisionEnter(Collision other)
    {
        //if player hit bird, restart level
        if(other.collider.CompareTag("Bird"))
        {
            SceneManager.LoadScene("Level 3");
        }
        //if player hit building, restart at checkpoint
        else if (other.collider.CompareTag("Building"))
        {
            timelineController.RestartAtCheckPoint();
        }
        else if (other.collider.CompareTag("ClipPlane"))
        {
            print("Hit ClipPlane");
            mainCam.farClipPlane = newFarClip;
        }
    }
}