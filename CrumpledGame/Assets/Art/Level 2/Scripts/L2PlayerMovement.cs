using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class L2PlayerMovement : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float speed = 10;
    public float jumpForce = 10f;
    float rollSpeed =5f;

    bool isGrounded;
    bool introFinished=false;
    bool fallSceneActive = false;
    public bool fallSceneStarted = false;
    bool onSticky = false;

    public Transform visual;
    public Sprite idleSprite;
    public Sprite rollSprite;
    public Sprite jumpSprite;

    float lastDirection = 1f;

    //Coroutine slowCoroutine;

    const float originalSpeed = 10;

    public int maxMove = 15;
    int moveCounter = 0;
    Quaternion originalRotation;
    Coroutine rotateBackCoroutine;
    bool isRotatingBack = false;

   L2PlayerSounds  l2PlayerSounds = new L2PlayerSounds();
    public L2IntroTrigger introTrigger;
    public bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = visual.GetComponent<SpriteRenderer>();
        originalRotation = visual.rotation;
        StartCoroutine(IntroFall());
    }

    void FixedUpdate()
    {
        if (!introFinished) return;
        if (fallSceneActive) return;
        if(!canMove)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        if (LetterOverlay.Instance != null && LetterOverlay.Instance.IsReadingLetter) return;

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

        if (Mathf.Abs(vx) > 0.1f)
        {
            if (isRotatingBack)
            {
                if (rotateBackCoroutine != null) StopCoroutine(rotateBackCoroutine);
                isRotatingBack = false;
            }
            moveCounter = Mathf.Min(moveCounter + 1, maxMove);
            sr.sprite = rollSprite;
            visual.localScale = new Vector3(lastDirection == 1 ? 1.2f : -1.2f,1.2f,1.2f);

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
            visual.localScale = new Vector3(lastDirection == 1 ? 1 : -1, 1, 1f);
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
            
            l2PlayerSounds.HitSound();
        }

        print(collision.gameObject.name);
        //L2KnockablePlatform kp = collision.gameObject.GetComponent<L2KnockablePlatform>();
        L2KnockablePlatform kp = collision.gameObject.GetComponentInParent<L2KnockablePlatform>();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndLvl"))
        {
            SceneManager.LoadScene(0);
        }
    }
    void OnMove(InputValue inputData)
    {
        direction = inputData.Get<Vector2>();

        // intro screen input
        if (!canMove && direction != Vector2.zero)
        {
            introTrigger.StartGame();
            return;
        }
    }
    void OnJump(InputValue value)
    {
        if(!canMove) return;
        if (onSticky) return;
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //for the sticky platforms
    public void ApplySlow (float slowMultiplier)
    {
        speed = 0.4f;
        Invoke("RemoveSlow", 2f);
        onSticky = true;

    }

    public void RemoveSlow()
    {
        
        speed = originalSpeed;  
        onSticky = false;

    }

    //for the intro fall
    IEnumerator IntroFall()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
        visual.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        rb.gravityScale = 5;
        sr.sprite = jumpSprite;

        // Wait until player hits the landing trigger
        yield return new WaitUntil(() => introFinished);

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
    }

    public void FinishIntro()
    {
        rb.gravityScale = 3;
        introFinished = true;
    }

    public void StartFallScene()
    {
        StartCoroutine(FallScene());
    }

    IEnumerator FallScene()
    {
        fallSceneActive = true;
        fallSceneStarted = true;

        // Disable movement and ignore platform collisions
        direction = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        sr.sprite = idleSprite;
        visual.localScale = Vector3.one;

        yield return new WaitForSeconds(1.3f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);

        sr.sprite = jumpSprite;
        rb.gravityScale = 5f;

        yield return new WaitUntil(() => !fallSceneActive);

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
        rb.gravityScale = 3f;
    }

    public void EndFallScene()
    {
        fallSceneActive = false;
    }
}

