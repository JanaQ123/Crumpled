using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
//using static System.IO.Enumeration.FileSystemEnumerable<TResult>;

public class L4Bird : MonoBehaviour
{
    [Header("References")]
    public GameObject bird;
    public Animator birdAnim;
    public Rigidbody2D playerRb;
    public L4PlayerMovement movementScript;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip warningSoundClip;

    [Header("Spawn Positions")]
    public float middleSpawnX = 190f;
    public float endSpawnX = 404f;

    public float flyHeight = 12f;
    public float exitX = -190f;

    [Header("Behavior")]
    public float idleThreshold = 1f;
    public float warningDelay = 1f;

    public float flySpeed = 14f;
    public float grabRange = 4f;

    [Header("Grab")]
    public float grabPause = 0.4f;
    public float carryDuration = 2f;

    [Header("Feet Offset")]
    public float playerHangOffsetY = -1f;

    bool cycleActive = false;
    float idleTimer = 0f;
    float originalGravity;

    public Transform startPosition;
    public bool startFollowing;
    void Start()
    {
        bird.SetActive(false);
        originalGravity = playerRb.gravityScale;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bird"),LayerMask.NameToLayer("Enemies"));
    }

    void Update()
    {
        if (cycleActive) return;

        bool playerStill =
            Mathf.Abs(playerRb.linearVelocity.x) < 1f;

        if (playerStill&&startFollowing)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleThreshold)
            {
                idleTimer = 0f;
                StartCoroutine(AttackCycle());
            }
        }
        else
        {
            idleTimer = 0f;
        }
    }

    IEnumerator AttackCycle()
    {
        cycleActive = true;

        audioSource.PlayOneShot(warningSoundClip);
        yield return new WaitForSeconds(warningDelay);

        SpawnBird();
        bird.SetActive(true);
        birdAnim.SetBool("isFlying", true);

        bool grabbed = false;

        // Fly toward player
        while (bird.transform.position.x > exitX)
        {
            Vector3 pos = bird.transform.position;

            // move left
            pos.x -= flySpeed * Time.deltaTime;

            // follow player's Y smoothly
            float targetY = transform.position.y + 2f;
            pos.y = Mathf.Lerp(pos.y, targetY,4f * Time.deltaTime);
            bird.transform.position = pos;

            bool playerStill =Mathf.Abs(playerRb.linearVelocity.x) < 0.5f;

            float distance =Vector2.Distance(bird.transform.position,transform.position);

            if (playerStill && distance <= grabRange)
            {
                grabbed = true;
                break;
            }

            yield return null;
        }

        
        if (grabbed)
        {
            birdAnim.SetBool("isFlying", false);
            birdAnim.SetTrigger("doGrab");

            // stop movement
            movementScript.canMove = false;
            playerRb.linearVelocity = Vector2.zero;
            playerRb.gravityScale = 0f;

            // short hover pause
            yield return new WaitForSeconds(grabPause);
            birdAnim.SetBool("isFlying", true);
            //float timer = 0f;

            //while (timer < carryDuration)
            //{
            //    timer += Time.deltaTime;

            //    // bird flies left
            //    bird.transform.position += Vector3.left * flySpeed * Time.deltaTime;

            //    // player hangs under bird
            //    

            //    yield return null;
            //}
            while (Vector2.Distance(bird.transform.position, startPosition.position) >= 0.01f)
            {
                bird.transform.position = Vector2.MoveTowards(
                    bird.transform.position,
                    startPosition.position,
                    50 * Time.deltaTime
                    );
                Vector3 hangPos = bird.transform.position + new Vector3(playerHangOffsetY, 0, 0);
                transform.position = hangPos;
                yield return null;

            }
            bird.transform.position = startPosition.position;
            // DROP PLAYER
            playerRb.gravityScale = originalGravity;
            movementScript.canMove = true;
        }

        // bird continues leaving
        while (bird.transform.position.x > exitX)
        {
            bird.transform.position += Vector3.left * flySpeed * Time.deltaTime;
            yield return null;
        }

        birdAnim.SetBool("isFlying", false);
        bird.SetActive(false);
        cycleActive = false;
    }

    void SpawnBird()
    {
        float spawnX = transform.position.x < 190f ? middleSpawnX : endSpawnX;

        bird.transform.position = new Vector3(spawnX,flyHeight,0f);
    }
}