using UnityEngine;
using System.Collections;

public class L4Snake : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Rigidbody2D playerRb;
    public L4PlayerMovement playerMovement;
    public Animator animator;

    [Header("Patrol")]
    public float patrolSpeed = 2f;
    public float patrolDistance = 4f;
    public float turnPause = 0.6f;

    [Header("Detection")]
    public float detectionRange = 3f;

    [Header("Attack")]
    public float biteCooldown = 0.7f;
    public float knockbackX = 6f;
    public float knockbackY = 2f;
    public float stunDuration = 0.5f;
    public Transform head; 
    [Header("Scale")]
    public float scaleSize = 2.5f;

    Vector3 startPos;

    bool movingLeft = true;
    bool turning = false;
    bool attacking = false;

    float biteTimer = 0f;

    float originalGravity;
    public bool enteredSnake;
    public bool snakeHit;
    bool startedHit = false;
    void Start()
    {
        startPos = transform.position;

        originalGravity = playerRb.gravityScale;
    }

    void Update()
    {
        print("did snake enter?" + enteredSnake);
        print("is snake in front?" + PlayerInFrontAndRange());
        biteTimer -= Time.deltaTime;

        if (enteredSnake)
        {
            snakeHit = PlayerInFrontAndRange();

        }
        else
        {
            snakeHit = false;

        }
        if (attacking) { return; }

        if (snakeHit)
        {
            print("detected");
            StartCoroutine(AttackLoop());
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {

        animator.SetBool("isWalking", true);
        if (turning) return;

        float dir = movingLeft ? -1f : 1f;

        transform.position += Vector3.right *dir *patrolSpeed *Time.deltaTime;

        FaceDirection(dir);

        float distanceFromStart =transform.position.x - startPos.x;

        if (movingLeft && distanceFromStart <= -patrolDistance)
        {
            StartCoroutine(TurnAround());
        }

        if (!movingLeft && distanceFromStart >= patrolDistance)
        {
            StartCoroutine(TurnAround());
        }
    }

    IEnumerator TurnAround()
    {
        turning = true;
        animator.SetBool("isWalking", false);

        yield return new WaitForSeconds(turnPause);

        movingLeft = !movingLeft;

        turning = false;
    }

    bool PlayerInFrontAndRange()
    {

        //// snake facing L
        if (movingLeft && player.position.x < transform.position.x) return true;

        // snake facing R
        if (!movingLeft && player.position.x > transform.position.x) return false;

        return false;
    }

    IEnumerator AttackLoop()
    {
        attacking = true;

        while (snakeHit)
        {
            print("I will bite");
            yield return StartCoroutine(BiteAttack());

            yield return new WaitForSeconds(biteCooldown);
        }

        attacking = false;
    }

    IEnumerator BiteAttack()
    {
        print("biting");

        //animator.SetTrigger("doBite");
        animator.SetBool("isBiting", true);

        playerMovement.canMove = false;

        float dir = movingLeft ? -1f : 1f;

        float biteTime = 0.2f;

        float timer = 0f;

        if (startedHit == false)
        {
            yield return new WaitForSeconds(0.5f);
            startedHit = true;
        }
            while (timer < biteTime)
        {
            timer += Time.deltaTime;
            dir = movingLeft ? -1f : 1f;


            // continuously push player
            player.position +=
                Vector3.right *
                dir *
                50 *
                Time.deltaTime;

            yield return null;
        }

        if (snakeHit == false)
        {
            playerMovement.canMove = true;

            animator.SetBool("isBiting", false);
            startedHit = false;
        }
      
    }
 

    void FaceDirection(float dir)
    {
        transform.localScale = new Vector3(dir > 0 ? -scaleSize : scaleSize,scaleSize,scaleSize);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(head.position,detectionRange);
    }
}