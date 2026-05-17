using UnityEngine;
using System.Collections;

public class L4Scorpion : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Rigidbody2D playerRb;
    public L4PlayerMovement playerMovement;

    Rigidbody2D rb;

    [Header("Detection")]
    public float detectionRange = 12f;
    public float loseRange = 8f;
    public float grabRange = 1.2f;

    [Header("Movement")]
    public float chaseSpeed = 20f;

    [Header("Grab")]
    public Transform grabPoint;
    public float playDuration = 2f;

    [Header("Throw")]
    public float throwForceX = 10f;
    public float throwForceY = 5f;

    Animator animator;

    //[Header("Player Hold Offset")]
    //public Vector2 holdOffset = new Vector2(-0.8f, 0.5f);

    bool chasing = false;
    bool busy = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (busy) return;

        float dist = Vector2.Distance (grabPoint.position,player.position);

        bool playerOnLeft =
            player.position.x < transform.position.x;


        if (!chasing)
        {
            if (dist <= detectionRange && playerOnLeft)
            {
                chasing = true;
                animator.SetBool("isWalking", true);
                print("chase");
            }
        }
        else
        {
            // Stop chase if escaped
            if (dist > loseRange || !playerOnLeft)
            {
                print("escaped");
                animator.SetBool("isWalking", false);
                chasing = false;
                rb.linearVelocity = new Vector2(0,rb.linearVelocity.y);
            }
        }

        // Chase movement
        if (chasing)
        {
            print("isChasing");
            //rb.linearVelocity = new Vector2(-chaseSpeed,rb.linearVelocity.y);
            transform.position +=Vector3.left * chaseSpeed *Time.deltaTime;

            // Grab
            if (dist <= grabRange)
            {
                StartCoroutine(GrabSequence());
            }
        }
        else
        {
            // Idle
            //rb.linearVelocity =new Vector2(0,rb.linearVelocity.y);
        }
    }

    IEnumerator GrabSequence()
    {
        print("grabbing");
        animator.SetBool("isWalking", false);
        busy = true;
        chasing = false;

        rb.linearVelocity = Vector2.zero;

        // Disable player movement & physics
        playerMovement.canMove = false;
        playerRb.linearVelocity = Vector2.zero;
        playerRb.gravityScale = 0f;

        // PICK UP PLAYER
        print("PICK UP PLAYER");
        float grabTime = 0.25f;
        float t = 0f;

        Vector3 startPos = player.position;

        Vector3 targetPos = grabPoint.position;

        while (t < 1f)
        {
            t += Time.deltaTime / grabTime;
            player.position = Vector3.Lerp(startPos,targetPos,t);

            yield return null;
        }

        // PLAY WITH PLAYER
        print("play with player");
        animator.SetBool("isPlaying", true);
        float timer = 0f;

        while (timer < playDuration)
        {
            timer += Time.deltaTime;

            // fake rolling motion
            player.position =targetPos + new Vector3(Mathf.Sin(timer * 10f) * 0.2f,Mathf.Cos(timer * 18f) * 0.1f,0);

            // rotate paper ball
            player.Rotate(0, 0, -720f * Time.deltaTime);

            yield return null;
        }

        // THROW PLAYER LEFT
        print("throw left");
        animator.SetBool("isPlaying", false);

        playerMovement.scorpianHit = true;
        playerRb.gravityScale = 5f;
        playerRb.linearVelocity = Vector2.zero;

        playerRb.AddForce(new Vector2(-throwForceX,throwForceY), ForceMode2D.Impulse);
        //playerRb.linearVelocity = new Vector2(-throwForceX,throwForceY );
        // restore movement
        playerMovement.canMove = true;

        yield return new WaitForSeconds(2f);
        playerMovement.scorpianHit = false;
        busy = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            grabPoint.position,
            grabRange
        );
    }
}