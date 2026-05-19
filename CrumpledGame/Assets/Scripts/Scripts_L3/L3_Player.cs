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
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] AudioClip birdHitSound;
    bool tornadoStart = false;
    float sideDuration = 2f;
    float timeElapsed = 0f;
    Vector3 startXPos;
    Vector3 targetX;
    SpriteRenderer sr;
    [SerializeField] ReadingLetter letterOverlay;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        tornadoStart = false;
    }
    void OnEnable()
    {
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<PlayerInput>().enabled = true;
    }
    void Update()
    {
        if(isDead) return;
        if (timelineController.timeline.time > 74f) 
        {
            moveSpeed = 20f;
        }
        PlayerMove();
        //if player is not in the center, reset to be in the center to start cutscene
        if(tornadoStart)
            ResetPosition();
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(birdHitSound);
                audioSource.PlayOneShot(playerHitSound);
            }
            timelineController.StopTimeline();
            Invoke("RestartLevel", 3f);
        }
        //if player hit building or clothes, restart at checkpoint
        else if (other.collider.CompareTag("Building") || other.collider.CompareTag("Clothes"))
        {
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(playerHitSound);
            }
            timelineController.StopTimeline();
            animator.SetTrigger("flying");
            Invoke("RestartCheckpoint", 3f);
        }
        //if player hits letter, collect it
        else if (other.collider.CompareTag("Letter_L3"))
        {
            letterOverlay.ShowLetter();
            other.gameObject.SetActive(false);
        }
        //if player hits tornado trigger, play cutscene
        else if (other.collider.CompareTag("TornadoCutscene"))
        {
            tornadoStart = true;
            Invoke("StartCutscene", 1f);
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
    void StartCutscene()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sortingLayerName = "Midground";
        timelineController.StopForCutscene();
    }
    void ResetPosition()
    {
        if(transform.localPosition.x != 0)
        {
            startXPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            targetX = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
            if(transform.localPosition.x < 0)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (timeElapsed < sideDuration)
                {
                    timeElapsed += Time.deltaTime;
                    float x = timeElapsed / sideDuration;
                    transform.localPosition = Vector3.Lerp(startXPos, targetX, x);
                }
            }
            if(transform.localPosition.x > 0)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if (timeElapsed < sideDuration)
                {
                    timeElapsed += Time.deltaTime;
                    float x = timeElapsed / sideDuration;
                    transform.localPosition = Vector3.Lerp(startXPos, targetX, x);
                }
            }
        }
    }
}