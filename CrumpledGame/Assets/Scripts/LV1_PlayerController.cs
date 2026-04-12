using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
public class LV1_PlayerController : MonoBehaviour
{
    public Vector3 direction;
    float x;
    float y;
    float savedSpeed = 12;
    float speed = 12;
    Vector3 directionX;
    int currentPosition = 1;
    public Vector3 targetPos;
    Vector3 targetScale;
    //Changing Lanes
    float[] scales = { 0.53f, 0.6f, 0.8f };
    float[] positions = { -2.9f, -4.7f, -7.2f };
    float[] limits = { 0.5f, 0.75f, 1, 1f, 1, 1 };
    public bool isSwitching = false;
    bool isGoingDownStairs=false;
    float[] stairPositions = {182.3f, 185.5f, 188.5f, 192f, 195.5f,198f};
    int nextStair = 0;
    float switchDuration = 0.1f;
    public bool canSwitchLane = true;
    float moveCounter=0;
    float maxMove=40;
    public bool gotKicked=false;
    int kickCounter;
    Vector3 originalShadowScale;
    Vector3 tempShadowScale;
    Vector3 originalPlayerScale;
    Quaternion originalRotation;
    float shadowRatio;
    float shadowY;
    float shadowOffset=1f;
    Rigidbody2D rb;
    [SerializeField] GameObject visual;
    [SerializeField] GameObject shadow;
    [SerializeField] CinemachineCamera mountainCam;
    public bool canMoveBack = true;
    bool inGum = false;
    bool inSewer = false;
    public bool inEndingGum = false;

    [Header("Hill Roll")]
    public bool isRolling = true;         // set false when roll is done
    public float rollGravity = 9.8f;
    public float maxRollSpeed = 10f;
    public float playerHalfHeight = 0.5f;
    public float dragMultiplier = 0.95f;
    Vector2 rollVelocity = Vector2.zero;

    void Start()
    {
        y = transform.position.y;
        visual.GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";
        visual.GetComponent<SpriteRenderer>().sortingOrder = 1;
        originalShadowScale = shadow.transform.localScale;
        originalPlayerScale = transform.localScale;
        shadowY = shadow.transform.localPosition.y;
        rb=this.GetComponent<Rigidbody2D>();  
      originalRotation=this.transform.rotation;
        currentPosition = 1;
        ChangePositions();
        mountainCam.Priority = 0;

    }
    void OnRestart()
    {
        SceneManager.LoadScene("Level 1");
    }
    void Update()
    {

        if (gotKicked)

            {
            visual.GetComponent<Animator>().SetBool("Pain", true);

            if (inGum) {EndGum(); }  
            if(inSewer) {   EndSewerCover(); }  
            shadow.transform.localPosition = new Vector3(transform.localPosition.x, shadowY, transform.localPosition.z);
              Vector3 minScale = tempShadowScale * 0.2f; 
              isSwitching = false;
                if (kickCounter < 65)
                {
                    transform.position += new Vector3(-1, 1, 0) * Time.deltaTime * speed;
                    kickCounter++;
                    float t = kickCounter / 65f; // 0 to 1
                    shadow.transform.localScale = Vector3.Lerp(originalShadowScale, minScale, t);

                }
                else if (kickCounter < 130)
                {
                    shadow.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    transform.position += new Vector3(-1, -1, 0) * Time.deltaTime * speed;
                    float t = (kickCounter - 65f) / 65f; // 0 to 1
                    shadow.transform.localScale = Vector3.Lerp(minScale, originalShadowScale, t);
                    kickCounter++;

                }
                else
                {
                    gotKicked = false;
                    isSwitching = true;
                    kickCounter = 0;
                    visual.GetComponent<Animator>().SetBool("Pain", false);

                }

            }
            else
            {
                shadow.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y-shadowOffset, transform.localPosition.z);
                shadowRatio = transform.localScale.x / originalPlayerScale.x;
                shadow.transform.localScale = originalShadowScale * shadowRatio;
            if (!inGum && !inSewer)
            {
                visual.GetComponent<Animator>().SetBool("Pain", false);

            }
        }

        if (directionX != Vector3.zero)
        {
            moveCounter = Mathf.Min(moveCounter + 1, maxMove);
            visual.transform.Rotate(0, 0, -directionX.x * speed * Time.deltaTime * moveCounter);
        }
        else
        {
            if (moveCounter >= maxMove)
            {
                StartCoroutine(RotateBack());
            }
            moveCounter = 0;
        }

        if (isSwitching)
        {
            targetPos.x = transform.localPosition.x;

            // transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime / switchDuration);
            transform.localPosition = Vector3.MoveTowards(
                 transform.localPosition,
                 targetPos,
                 9 * Time.deltaTime //speed is 9
             );
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime / switchDuration);


            if (Mathf.Abs(transform.localPosition.y - targetPos.y) < 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, targetPos.y, 0);
                transform.localScale = targetScale;
                isSwitching = false;
            }


        }
        if (nextStair >=5)
        {
            float backLimit = stairPositions[nextStair - 1] + limits[nextStair - 1];
            transform.position = new Vector3(Mathf.Max(transform.position.x, backLimit), transform.position.y, 0);
        }
        if (isGoingDownStairs)
        {
            speed = 8;

            if (nextStair > 0)
            {
                float backLimit = stairPositions[nextStair - 1] + limits[nextStair - 1];
                transform.position = new Vector3(Mathf.Max(transform.position.x, backLimit), transform.position.y, 0);
            }

            //if(nextStair < stairPositions.Length -)
            if (nextStair <= 6 && transform.position.x > stairPositions[nextStair] && !isSwitching)
            {
                if (nextStair == 3)
                {
                    targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 2f, 0); // actually move Y!
                }
                else
                {
                    targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 1.5f, 0);

                }
                isSwitching = true;

                targetScale = transform.localScale;
                nextStair++;
                switchDuration = 0.15f;
            }
        }
    }
    IEnumerator RotateBack()
    {
        Quaternion currentRot = visual.transform.rotation;
        float t = 0f;
        float duration = 0.5f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            visual.transform.rotation = Quaternion.Slerp(currentRot, originalRotation, t);
            yield return null;
        }

        visual.transform.rotation = originalRotation;
    }
    void OnMove(InputValue data)
    {
        direction = new Vector3(data.Get<Vector2>().x, data.Get<Vector2>().y, 0);
        directionX = new Vector3(Mathf.RoundToInt(direction.x), 0, 0);
        if (!canMoveBack && direction.x < 0) direction.x = 0; // block left input
        currentPosition -= Mathf.RoundToInt(direction.y); //changes float direction value to int
        currentPosition = Mathf.Clamp(currentPosition, 0, 2); //clamps to be in array
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, data.Get<Vector2>().y), 2f, ~LayerMask.GetMask("Player"));

        if (hit.collider == null && data.Get<Vector2>().y != 0 && !isSwitching&&canSwitchLane) { 
            ChangePositions(); //switch lanes
        }
        Vector2 input = data.Get<Vector2>();
    }

    public void Kick()
    {
        gotKicked = true;
        tempShadowScale = shadow.transform.localScale;
        shadowY = shadow.transform.localPosition.y;

    }
    public void ChangePositions()
    {
        if (currentPosition == 0)
        {
            visual.GetComponent<SpriteRenderer>().sortingLayerName = "Midground";
            visual.GetComponent<SpriteRenderer>().sortingOrder =2;
            //shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Midground";
            //shadow.GetComponent<SpriteRenderer>().sortingOrder = 1;
            shadowOffset = 0.75f;
        }
        if (currentPosition == 1)
        {
            visual.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            visual.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            //shadow.GetComponent<SpriteRenderer>().sortingOrder = 1;
            shadowOffset = 1f;

        }
        if (currentPosition == 2) 
        {

            visual.GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";
            visual.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";
            //shadow.GetComponent<SpriteRenderer>().sortingOrder = 1;
            shadowOffset = 1.1f;


        }
        float yOffset = isGoingDownStairs ? -nextStair-1 : 0f;
        if (isGoingDownStairs && nextStair == 4) yOffset -= 1f;
        targetPos = new Vector3(transform.localPosition.x, positions[currentPosition] + yOffset, 0);
        targetScale = new Vector3(scales[currentPosition], scales[currentPosition], scales[currentPosition]);
        isSwitching = true;
        switchDuration = 0.15f;
    }

    public void setStairControl()
    {
        isGoingDownStairs = !isGoingDownStairs;
        if (!isGoingDownStairs) 
        {
            transform.localPosition = new Vector3(transform.localPosition.x, positions[currentPosition], 0);
            targetPos = new Vector3(transform.localPosition.x, positions[currentPosition], 0);
            targetScale = new Vector3(scales[currentPosition], scales[currentPosition], scales[currentPosition]);
            speed = savedSpeed;
        }
        isSwitching = false;

    }

    public bool getStairStatus()
    {
        return isGoingDownStairs;
    }

   public void StartSewerCover()
    {
        speed = speed/10;
        canSwitchLane = false;
        visual.GetComponent<Animator>().SetBool("Pain", true);
        inSewer = true;

    }

    public void EndSewerCover()
    {
        visual.GetComponent<Animator>().SetBool("Pain", false);
        canSwitchLane = true;
        speed = savedSpeed;
        inSewer = false;

    }

    public void StartGum()
    {

        speed = 0;
        canSwitchLane = false;
        inGum = true;
        //this.transform.localScale=new Vector3(this.transform.localScale.x+0.2f, this.transform.localScale.y, this.transform.localScale.z);
        visual.GetComponent<Animator>().SetBool("Pain", true);
        Invoke("EndGum", 3f);

    }
    void EndGum()
    {
        inGum = false;
        canSwitchLane = true;
        speed = savedSpeed;
        //this.transform.localScale = new Vector3(this.transform.localScale.y, this.transform.localScale.y, this.transform.localScale.z);
        visual.GetComponent<Animator>().SetBool("Pain", false);


    }
    void FixedUpdate()
    {
        if (isRolling)
        {
            rb.AddForce(new Vector2(direction.x * speed, 0));
            return;
        }

        else

        {

            Vector2 newPos = rb.position + new Vector2(direction.x, 0) * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);

        }



        //Vector2 newPos = rb.position + new Vector2(direction.x, 0) * speed * Time.fixedDeltaTime;

        //rb.MovePosition(newPos);
    }

    //void RollDownHill()
    //{
    //    Vector2 rayOrigin = rb.position + Vector2.up * 0.1f;
    //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 3f, LayerMask.GetMask("Ground"));

    //    Debug.DrawRay(rayOrigin, Vector2.down * 3f, Color.red); // visualize in scene view

    //    if (hit.collider != null)
    //    {
    //        Vector2 slope = new Vector2(hit.normal.y, -hit.normal.x);

    //        float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
    //        if (slopeAngle > 5f)
    //        {
    //            // is the slope going down in the direction of movement?
    //            bool goingDownhill = (slope.x > 0 && rollVelocity.x > 0) ||
    //                                 (slope.x < 0 && rollVelocity.x < 0);

    //            if (goingDownhill)
    //            {
    //                // downhill - apply light gravity, heavy drag
    //                rollVelocity += slope * rollGravity * Time.fixedDeltaTime;
    //                rollVelocity *= 0.92f; // strong drag going down
    //            }
    //            else
    //            {
    //                // uphill - help the player climb, no gravity fighting them
    //                rollVelocity += slope * rollGravity * 0.2f * Time.fixedDeltaTime; // barely any gravity
    //                rollVelocity *= 0.98f; // light drag going up
    //            }
    //        }
    //        else
    //        {
    //            // flat ground friction
    //            rollVelocity.x = Mathf.MoveTowards(rollVelocity.x, 0, rollGravity * Time.fixedDeltaTime);
    //        }


    //        // ✅ player input nudges him forward/back on top of slope gravity
    //        rollVelocity.x += direction.x * speed * Time.fixedDeltaTime;

    //        rollVelocity.x = Mathf.Clamp(rollVelocity.x, -maxRollSpeed, maxRollSpeed);
    //        rollVelocity.y = Mathf.Clamp(rollVelocity.y, -maxRollSpeed, maxRollSpeed);

    //        Vector2 newPos = rb.position + rollVelocity * Time.fixedDeltaTime;
    //        newPos.y = hit.point.y + playerHalfHeight;
    //        rb.MovePosition(newPos);
    //    }
    //    else
    //    {
    //        rollVelocity += Vector2.down * rollGravity * Time.fixedDeltaTime;
    //        rb.MovePosition(rb.position + rollVelocity * Time.fixedDeltaTime);
    //    }
    //}
    public void StartRolling()

    {
        rb = this.GetComponent<Rigidbody2D>();

        rb.gravityScale = 1f;
        rb.angularDamping = 0.05f;
        rb.linearDamping = 0.5f;

        rb.constraints = RigidbodyConstraints2D.None; // unfreeze everything

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        isRolling = true;

        canSwitchLane = false;



    }

    public void StopRolling()

    {
        rb = this.GetComponent<Rigidbody2D>();

        isRolling = false;

        mountainCam.Priority = 0;

        rb.WakeUp();



    }

    public void StartLanes()

    {

        currentPosition = 1;

        ChangePositions();

        canSwitchLane = true;



    }
}
