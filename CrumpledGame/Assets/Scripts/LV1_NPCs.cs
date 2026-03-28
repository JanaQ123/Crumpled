using UnityEngine;
using UnityEngine.Identifiers;

public class LV1_NPCs : MonoBehaviour
{
    float speed = 9f;
    bool isWalking = true;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(0.8f, 1.2f);
        anim.Play("DefaultWalk", 0, Random.Range(0f, 1f));
    }
    void Update()
    {
        if (isWalking)
            transform.parent.Translate(Vector3.left * speed * Time.deltaTime);
    }
    public void StartWalking()
    {
        isWalking = true;
    }
    public void StopWalking()
    {
        isWalking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("entered");
        if (collision.gameObject.tag == "Player")
        { collision.gameObject.GetComponent<LV1_PlayerController>().Kick(); }

        if (collision.gameObject.tag == "Stairs")
        {
            anim.SetBool("Stop", true);
            speed = 0;
            Invoke("GoUpStairs", 1f);

        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { collision.gameObject.GetComponent<LV1_PlayerController>().Kick(); }
       

    }
    void GoUpStairs()
    {
        anim.SetBool("Stairs", true);
        speed = 1;

    }
    public void OnStairsAnimationEnd()
    {
        Destroy(transform.parent.gameObject);

    }


}
