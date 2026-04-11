using UnityEngine;

public class LV1_CameraTarget : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool resetTarget=false;
    float originalY;
    void Start()
    {
        originalY=this.transform.localPosition.y;
    }
    void Update()
    {
        if (player.GetComponent<LV1_PlayerController>().getStairStatus())
        {
            //if (Mathf.Abs(this.transform.localPosition.y - -9.5f) > 0.01f)
            //{
            //    float newY = Mathf.MoveTowards(this.transform.localPosition.y, -9.5f, 5 * Time.deltaTime);
            //    this.transform.localPosition = new Vector3(this.transform.localPosition.x, newY, 0);
            //}

            Vector3 targetPos = new Vector3(
                188,
                -9.5f,
                -3
                    );

            transform.position = Vector3.MoveTowards(
             transform.position,
             targetPos,  // your hardcoded world position
             6 * Time.deltaTime
             );

            if (Vector3.Distance(transform.position, targetPos) <= 0.01f)
            {
                transform.position = targetPos;
            }
        }
        //else if (resetTarget)
        //{
        //    Vector3 targetPos = new Vector3(
        //    player.transform.localPosition.x,
        //    originalY,
        //    0
        //        );

        //    transform.localPosition = Vector3.MoveTowards(
        //        transform.localPosition,
        //        targetPos,
        //        10 * Time.deltaTime
        //    );

        //    if (Vector3.Distance(transform.localPosition, targetPos) <= 0.01f)
        //    {
        //        transform.localPosition = targetPos; // snap exact
        //        resetTarget = false;
        //    }
        //}
        else
        {
           
            this.transform.localPosition = new Vector3(player.transform.localPosition.x, originalY, 0);
        }
    }
    public void ChangeY()
    {
        originalY = -4.6f;
    }
}
