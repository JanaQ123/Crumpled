using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 10f;
    float playerPosX;
    Vector3 direction;
    Vector3 playerPos;  
    public TimelineController timelineController;
    Camera mainCam;
    float currentFarClip = 50f;
    float newFarClip = 100f;
    void Start()
    {
        mainCam = Camera.main;
        mainCam.farClipPlane = currentFarClip;
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
        //if player hit building or clothes, restart at checkpoint
        else if (other.collider.CompareTag("Building") || other.collider.CompareTag("Clothes"))
        {
            print("I collided with " + other.collider.tag);
            timelineController.RestartAtCheckPoint();
        }
        else if (other.collider.CompareTag("ClipPlane"))
        {
            mainCam.farClipPlane = newFarClip;
        }
    }
    public void OnRestart(InputValue value)
    {
        if (value.isPressed)
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}