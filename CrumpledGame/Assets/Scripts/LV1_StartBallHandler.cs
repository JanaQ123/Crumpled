using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Identifiers;
using Unity.Cinemachine;
public class LV1_StartBallHandler : MonoBehaviour
{
    Rigidbody2D rb;
    public bool canMove=false;
    private bool wasMoving = false;
    Animator anim;
    public CinemachineCamera mCam;
    public GameObject keypad;
    [SerializeField] AudioSource grass;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = rb.GetComponentInChildren<Animator>();
        keypad.SetActive(false);
        grass.Play();

    }


    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        bool isMoving = rb.linearVelocity.magnitude > 0.1f;

        if (wasMoving && !isMoving && !canMove)
        {
            StartCoroutine(SettleAndWakeUp());
        }

        wasMoving = isMoving;


            if (canMove)
        {
            grass.Play();
            if (Keyboard.current.dKey.isPressed)
            {
                rb.AddForce(Vector2.right * Time.deltaTime * 150);
            }
            if (Keyboard.current.aKey.isPressed)
            {
                rb.AddForce(Vector2.left * Time.deltaTime * 150);
            }

            if (Keyboard.current.rightArrowKey.isPressed)
            {
                rb.AddForce(Vector2.right * Time.deltaTime * 200);
            }

            if (Keyboard.current.leftArrowKey.isPressed)
            {
                rb.AddForce(Vector2.left * Time.deltaTime * 200);
            }
        }
    }
    IEnumerator SettleAndWakeUp()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        float duration = 0.4f;
        float elapsed = 0f;
        float startAngle = transform.eulerAngles.z;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.LerpAngle(startAngle, 0f, elapsed / duration);
            transform.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }
        StartCoroutine(LerpCameraOffset(new Vector3(0, 0, -14), 0.5f));
        transform.eulerAngles = Vector3.zero;
        anim.SetTrigger("Open");
        Invoke("AllowMove", 6.5f);

    }
    public void AllowMove()
    {
        grass.Stop();
        StartCoroutine(LerpCameraOffset(new Vector3(0, 0, -10), 0.5f));
        print("could move");
        canMove = true;
        rb.constraints = RigidbodyConstraints2D.None;
        keypad.SetActive(true);
    }

    IEnumerator LerpCameraOffset(Vector3 targetOffset, float duration)
    {
        var follow = mCam.GetComponent<CinemachineFollow>();
        Vector3 startOffset = follow.FollowOffset;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            follow.FollowOffset = Vector3.Lerp(startOffset, targetOffset, elapsed / duration);
            yield return null;
        }

        follow.FollowOffset = targetOffset;
    }

}
