using System.Collections;
using UnityEngine;
using UnityEngine.Identifiers;
using UnityEngine.UIElements;

public class LV1_NPCs : MonoBehaviour
{
    float speed = 9f;
    bool isWalking = false;
    Animator anim;
    bool stepOver = false;
    float stepDownAmount = 2f;
    bool steppedDown = false;
    float originalY;
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.speed = Random.Range(0.8f, 1.2f);
        originalY = transform.parent.position.y;
    }
    void Update()
    {
        Debug.DrawRay(transform.position+new Vector3(-4, 2, 0), -transform.right * 5f, Color.red);
        if (isWalking)
        {
            transform.parent.Translate(Vector3.left * speed * Time.deltaTime);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-2, 2, 0), -transform.right, 5f, LayerMask.GetMask("Obstacle"));
        if (hit.collider != null && !stepOver)
        {
            stepOver = true;
            StartCoroutine(StepOverObstacle());
        }
     
    }
    public void StartWalking()
    {
        speed = 9;
        anim.SetBool("Stairs", false);
        anim.Play("DefaultWalk", 0, Random.Range(0f, 1f));
        isWalking = true;
    }
    public void StopWalking()
    {
        isWalking = false;

    }

    IEnumerator StepOverObstacle()
    {
        float targetY = originalY - stepDownAmount;
        if (steppedDown == false)
        {
            steppedDown = true;

            while (Mathf.Abs(transform.parent.position.y - targetY) > 0.01f)
            {
                float newY = Mathf.MoveTowards(transform.parent.position.y, targetY, speed * Time.deltaTime);
                transform.parent.position = new Vector3(transform.parent.position.x, newY, transform.parent.position.z);
                yield return null;
            }

        }
        
       yield return new WaitForSeconds(1f);
        
        while (Physics2D.Raycast(transform.position, transform.up, 2f, LayerMask.GetMask("Obstacle")))
        {
            yield return null; 
        }
        while (Mathf.Abs(transform.parent.position.y - originalY) > 0.01f)
        {
            float newY = Mathf.MoveTowards(transform.parent.position.y, originalY, speed * Time.deltaTime);
            transform.parent.position = new Vector3(transform.parent.position.x, newY, transform.parent.position.z);
            yield return null;
        }
        transform.parent.position = new Vector3(transform.parent.position.x, originalY, transform.parent.position.z);
        steppedDown = false;
        stepOver = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { collision.gameObject.GetComponent<LV1_PlayerController>().Kick(); }

        if (collision.gameObject.tag == "Stairs")
        {

            anim.speed = 1;
            anim.SetBool("Stairs", true);
            speed = 1;

        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { collision.gameObject.GetComponent<LV1_PlayerController>().Kick(); }
       

    }
    void GoUpStairs()
    {
        anim.speed = 1;
        anim.SetBool("Stairs", true);
        speed = 1;

    }

}
