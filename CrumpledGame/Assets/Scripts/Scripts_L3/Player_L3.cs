using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 11.5f;
    float playerPosX;
    Vector3 direction;
    Vector3 playerPos;  
    public TimelineController timelineController;
    Animator isFlying;
    public bool isDead = false;
    void Start()
    {
        isFlying = GetComponentInChildren<Animator>();
    }
    void Update()
    {
       if(isDead) return;
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
            //other.gameObject.GetComponent<Renderer>().enabled = false;
            //other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, other.gameObject.transform.position + new Vector3(0, 10, 0), 8 * Time.deltaTime);
            other.gameObject.SetActive(false);
            timelineController.StopTimeline();
            Invoke("RestartLevel", 3f);
        }
        //if player hit building or clothes, restart at checkpoint
        else if (other.collider.CompareTag("Building") || other.collider.CompareTag("Clothes"))
        {
            timelineController.StopTimeline();
            isFlying.SetTrigger("flying");
            Invoke("RestartCheckpoint", 3f);
        }
        //if player hits letter, collect it
        else if (other.collider.CompareTag("Letter_L3"))
        {
            other.gameObject.SetActive(false);
        }
    }
    public void OnRestart(InputValue value)
    {
        if (value.isPressed)
        {
            SceneManager.LoadScene("Level 3");
        }
    }
    void RestartLevel()
    {
        SceneManager.LoadScene("Level 3");
        isFlying.Play("flying"); 
    }
    void RestartCheckpoint()
    {
        timelineController.RestartAtCheckPoint();
    }
}