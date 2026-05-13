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

    [Header("Scale")]
    public float scaleSize = 2.5f;

    Vector3 startPos;

    bool movingLeft = true;
    bool turning = false;
    bool attacking = false;

    float biteTimer = 0f;

    float originalGravity;

    void Start()
    {
        startPos = transform.position;

        originalGravity = playerRb.gravityScale;
    }

    void Update()
    {
        biteTimer -= Time.deltaTime;

        if (attacking) return;

        bool playerDetected = PlayerInFrontAndRange();

        if (playerDetected)
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
        float dist = Vector2.Distance(transform.position,player.position);

        if (dist > detectionRange) return false;

        // snake facing L
        if (movingLeft && player.position.x < transform.position.x) return true;
       
        // snake facing R
        if (!movingLeft && player.position.x > transform.position.x) return true;

        return false;
    }

    IEnumerator AttackLoop()
    {
        attacking = true;

        while (PlayerInFrontAndRange())
        {
            yield return StartCoroutine(BiteAttack());

            yield return new WaitForSeconds(biteCooldown);
        }

        attacking = false;
    }

    IEnumerator BiteAttack()
    {
        print("biting");
        // disable movement
        playerMovement.canMove = false;

        // reset velocity
        playerRb.linearVelocity = Vector2.zero;

        // throw direction
        float dir = movingLeft ? -1f : 1f;

        //playerMovement.externalForceActive = true;

        // apply knockback
        playerMovement.beingHit = true;
        playerRb.linearVelocity = new Vector2(dir * knockbackX,knockbackY);
        yield return new WaitForSeconds(stunDuration);

        //playerRb.linearVelocity = new Vector2(0,playerRb.linearVelocity.y);
        //playerMovement.externalForceActive = false;
        playerMovement.canMove = true;
        playerMovement.beingHit = false;
    }

    void FaceDirection(float dir)
    {
        transform.localScale = new Vector3(dir > 0 ? -scaleSize : scaleSize,scaleSize,scaleSize);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position,detectionRange);
    }
}