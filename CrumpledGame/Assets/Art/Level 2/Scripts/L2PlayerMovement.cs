using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class L2PlayerMovement : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float speed = 10;
    public float jumpForce = 10f;
    float rollSpeed =5f;

    bool isGrounded;

    public Transform visual;
    public Sprite idleSprite;
    public Sprite rollSprite;
    public Sprite jumpSprite;

    float lastDirection = 1f;

    Coroutine slowCoroutine;

    const float originalSpeed = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = visual.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        float vx = rb.linearVelocity.x;

        if (vx > 0.1f) lastDirection = 1f;
        else if (vx < -0.1f) lastDirection = -1f;



        if (!isGrounded)
        {
            sr.sprite = jumpSprite;
            visual.localScale = new Vector3(lastDirection == 1 ? 1.2f : -1.2f, 1.2f, 1.2f);


            return;
        }

        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            sr.sprite = rollSprite;
            visual.localScale = new Vector3(lastDirection == 1 ? 1.2f : -1.2f,1.2f,1.2f);
            visual.Rotate(0, 0, rb.linearVelocity.x * rollSpeed);

        }
        else
        {
            sr.sprite = idleSprite; 
            visual.localScale = new Vector3(lastDirection == 1 ? 1 : -1, 1, 1f);
            visual.rotation = Quaternion.identity;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
        
        L2KnockablePlatform kp = collision.gameObject.GetComponent<L2KnockablePlatform>();
        if (kp != null)
        {
            kp.Hit();
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
        direction = inputData.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    //fot the sticky platforms
    public void ApplySlow (float slowMultiplier)
    {
        speed = 0.4f;
        Invoke("RemoveSlow", 2f);

    }

    public void RemoveSlow()
    {
        
        speed = originalSpeed;  

    }

    

}

