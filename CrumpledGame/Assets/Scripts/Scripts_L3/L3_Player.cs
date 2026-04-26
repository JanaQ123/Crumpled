using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class L3_Player : MonoBehaviour
{
    float moveSpeed = 12.5f;
    float playerPosX;
    Vector3 direction;
    Vector3 playerPos;  
    public L3_TimelineController timelineController;
    Animator animator;
    public bool isDead = false;
    AudioSource audioSource;
    [SerializeField] AudioClip crumpledSound;
    [SerializeField] AudioClip birdHitSound;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(isDead) return;
        if (timelineController.timeline.time > 74f) 
        {
            moveSpeed = 20f;
        }
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
            audioSource.PlayOneShot(birdHitSound);
            timelineController.StopTimeline();
            Invoke("RestartLevel", 3f);
        }
        //if player hit building or clothes, restart at checkpoint
        else if (other.collider.CompareTag("Building") || other.collider.CompareTag("Clothes"))
        {
            audioSource.PlayOneShot(crumpledSound);
            timelineController.StopTimeline();
            animator.SetTrigger("flying");
            Invoke("RestartCheckpoint", 3f);
        }
        //if player hits letter, collect it
        else if (other.collider.CompareTag("Letter_L3"))
        {
            other.gameObject.SetActive(false);
        }
        //if player hits tornado trigger, play cutscene
        else if (other.collider.CompareTag("TornadoCutscene"))
        {
            timelineController.StopForCutscene();
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
        animator.Play("flying"); 
    }
    void RestartCheckpoint()
    {
        timelineController.RestartAtCheckPoint();
    }
}