using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class LV1_PlayerController : MonoBehaviour
{
    Vector3 direction;
    float x;
    float y;
    float speed = 14;
    Vector3 directionX;
    int currentPosition = 1;
    public Vector3 targetPos;
    Vector3 targetScale;
    //Changing Lanes
    float[] scales = { 0.53f, 0.71f, 0.87f };
    float[] positions = { -2.9f, -4.4f, -7f };
    float[] limits = { 0.5f, 0.75f, 1, 1f, 1, 1 };
    public bool isSwitching = false;
    bool isGoingDownStairs=false;
    float[] stairPositions = {182.3f, 185.5f, 188.5f, 192f, 195.5f,198f};
    int nextStair = 0;
    float switchDuration = 0.1f;
    bool canSwitchLane = true;
    float moveCounter=0;
    float maxMove=30;

    [SerializeField] GameObject visual;
    void Start()
    {
        y = transform.position.y;
        visual.GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";
        visual.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    void Update()
    {
        if (directionX != Vector3.zero)
        {
            moveCounter++;
            if (moveCounter >= maxMove) { moveCounter = maxMove; }
        }
        else
        {
            moveCounter = 0;
        }

        transform.Translate(directionX * speed * Time.deltaTime);
        visual.transform.Rotate(0, 0, -directionX.x * speed * Time.deltaTime * moveCounter);

        if (isSwitching)
        {
           targetPos.x = transform.localPosition.x;

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime / switchDuration);
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

            if (nextStair < 6 && transform.position.x > stairPositions[nextStair] && !isSwitching)
            {
                if (nextStair == 5)
                {
                    print("final stair");
                    targetPos = new Vector3(transform.localPosition.x, positions[currentPosition], 0); // actually move Y!
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
    void OnMove(InputValue data)
    {
        direction = new Vector3(data.Get<Vector2>().x, data.Get<Vector2>().y, 0);
        directionX = new Vector3(Mathf.RoundToInt(direction.x), 0, 0); //Normalizes the x vector to allow presisng of 2 buttons
        currentPosition -= Mathf.RoundToInt(direction.y); //changes float direction value to int
        currentPosition = Mathf.Clamp(currentPosition, 0, 2); //clamps to be in array
        if (data.Get<Vector2>().y != 0 && !isSwitching&&canSwitchLane) { 
            ChangePositions(); //switch lanes
        }
       
    }
    void ChangePositions()
    {
        if (currentPosition == 1)
        {
            visual.GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";
            visual.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            visual.GetComponent<SpriteRenderer>().sortingLayerName = "Player";


        }
        float yOffset = isGoingDownStairs ? -nextStair-1 : 0f;
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
            isSwitching = false;
            speed = 14;
        }
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


    }

    public void EndSewerCover()
    {
        visual.GetComponent<Animator>().SetBool("Pain", false);
        canSwitchLane = true;
        speed = speed * 10;

    }

    public void StartGum()
    {
        speed = 0;
        canSwitchLane = false;
        this.transform.localScale=new Vector3(this.transform.localScale.x+0.2f, this.transform.localScale.y, this.transform.localScale.z);
        visual.GetComponent<Animator>().SetBool("Pain", true);
        Invoke("EndGum", 3f);

    }
    void EndGum()
    {
        canSwitchLane = true;
        speed = 14;
        this.transform.localScale = new Vector3(this.transform.localScale.y, this.transform.localScale.y, this.transform.localScale.z);
        visual.GetComponent<Animator>().SetBool("Pain", false);


    }
}
