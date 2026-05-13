using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class L4PlayerMovement : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float speed = 20;
    public float jumpForce = 10f;
    float rollSpeed = 3f;

    bool isGrounded;

    public Transform visual;
    public Sprite idleSprite;
    public Sprite rollSprite;
    public Sprite jumpSprite;

    float lastDirection = 1f;

    Coroutine slowCoroutine;
    public int maxMove = 15;
    int moveCounter = 0;
    Quaternion originalRotation;
    Coroutine rotateBackCoroutine;

    public L4Snake snake;
    bool isRotatingBack = false;

    [HideInInspector] public bool canMove = true;
    //[HideInInspector] public bool externalForceActive = false;

    float forceValue = 0.95f;
    public bool beingHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = visual.GetComponent<SpriteRenderer>();
        originalRotation = visual.rotation;
    }

    void FixedUpdate()
    {
        float targetX = 0f;

        if (canMove)
        {
            targetX = direction.x * speed;
        }

        if (beingHit)
        {
            forceValue = 0.5f;
            print("95");
        }
        else if (snake.snakeHit)
        {
            forceValue = 0.95f;

        }
        else
        {
            forceValue = 0.35f;
            print("35");
        }
        rb.linearVelocity = new Vector2(targetX + rb.linearVelocity.x * forceValue, rb.linearVelocity.y);

        float vx = rb.linearVelocity.x;

        if (vx > 0.1f) lastDirection = 1f;
        else if (vx < -0.1f) lastDirection = -1f;



        if (!isGrounded)
        {
            sr.sprite = jumpSprite;
            visual.localScale = new Vector3(lastDirection == 1 ? 1 : -1, 1, 1f);

            return;
        }

        if (Mathf.Abs(vx) > 0.1f)
        {
            if (isRotatingBack)
            {
                if (rotateBackCoroutine != null) StopCoroutine(rotateBackCoroutine);
                isRotatingBack = false;
            }
            moveCounter = Mathf.Min(moveCounter + 1, maxMove);
            sr.sprite = rollSprite;
            visual.localScale = new Vector3(lastDirection == 1 ? 1 : -1, 1, 1f);

            float momentumFactor = (float)moveCounter / maxMove;
            visual.Rotate(0, 0, -vx * rollSpeed * Time.fixedDeltaTime * moveCounter);

        }
        else
        {
            if (moveCounter >= maxMove)
            {
                // Rolled a lot — snap eyes back to front
                if (!isRotatingBack)
                {
                    rotateBackCoroutine = StartCoroutine(RotateBack());
                }
            }
            else
            {
                // Short tap — just snap instantly, no full rotation happened
                if (!isRotatingBack)
                {
                    visual.rotation = originalRotation;
                }
            }
            moveCounter = 0;
            sr.sprite = idleSprite;
            visual.localScale = new Vector3(lastDirection == 1 ? 1.2f : -1.2f, 1.2f, 1.2f);
            //visual.rotation = Quaternion.identity;
        }

    }
    IEnumerator RotateBack()
    {
        isRotatingBack = true;
        Quaternion currentRot = visual.rotation;
        float t = 0f;
        float duration = 0.3f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            visual.rotation = Quaternion.Slerp(currentRot, originalRotation, t);
            yield return null;
        }

        visual.rotation = originalRotation;
        isRotatingBack = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    void OnMove(InputValue inputData)
    {
        if (!canMove)
        {
            direction = Vector2.zero;
            return;
        }

        direction = inputData.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!canMove) return;
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
