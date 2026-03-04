using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class LV1_PlayerController : MonoBehaviour
{
    Vector3 direction;
    float x;
    float y;
    float speed = 10;
    Vector3 directionX;
    int currentPosition = 1;
    public Vector3 targetPos;
    Vector3 targetScale;
    //Changing Lanes
    float[] scales = { 0.53f, 0.71f, 0.87f };
    float[] positions = { -2.9f, -4.4f, -7f };

    public bool isSwitching = false;
    bool isGoingDownStairs=false;
    float[] stairPositions = {182.3f, 185.8f, 188f, 192.5f, 195f,198f};
    int nextStair = 0;

    void Start()
    {
        y = transform.position.y;
    }
    void Update()
    {
        transform.Translate(directionX * speed * Time.deltaTime);

        if (isSwitching)
        {
           targetPos.x = transform.localPosition.x;

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime / 0.1f);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime / 0.1f);
            if (Mathf.Abs(transform.localPosition.y - targetPos.y) < 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, targetPos.y, 0);
                transform.localScale = targetScale;
                isSwitching = false;
            }
        }
        if (isGoingDownStairs)
        {
            if(nextStair < 6 && transform.position.x > stairPositions[nextStair])
            {
                transform.position= new Vector3(transform.position.x, transform.position.y-1.5f,0);
                nextStair++;
            }
        }
    }
    void OnMove(InputValue data)
    {
        direction = new Vector3(data.Get<Vector2>().x, data.Get<Vector2>().y, 0);
        directionX = new Vector3(Mathf.RoundToInt(direction.x), 0, 0); //Normalizes the x vector to allow presisng of 2 buttons
        currentPosition -= Mathf.RoundToInt(direction.y); //changes float direction value to int
        currentPosition = Mathf.Clamp(currentPosition, 0, 2); //clamps to be in array
        if (data.Get<Vector2>().y != 0 && !isSwitching) { 
            ChangePositions(); //switch lanes
        }
    }
    void ChangePositions()
    {
        if (currentPosition == 1)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";
            GetComponent<SpriteRenderer>().sortingOrder = 1;


        }
        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Player";


        }
        targetPos = new Vector3(transform.localPosition.x, positions[currentPosition], 0);
        targetScale = new Vector3(scales[currentPosition], scales[currentPosition], scales[currentPosition]);
        isSwitching = true;
    }

    public void setStairControl()
    {
        isGoingDownStairs = !isGoingDownStairs;
    }

    public bool getStairStatus()
    {
        return isGoingDownStairs;
    }

   
}
