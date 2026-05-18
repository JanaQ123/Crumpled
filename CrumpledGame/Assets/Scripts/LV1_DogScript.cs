using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class LV1_DogScript : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerShadow;

    [SerializeField] GameObject dogHead;
    [SerializeField] GameObject dogHeadBone;
    [SerializeField] PlayableDirector endingTimeline;
    [SerializeField] AudioClip Bark;


    bool isFollow =false;
    bool isAttached = false;
    float speed = 17;
    Animator anim;
    AudioSource audio;
    void Start()
    {
        anim=dog.GetComponent<Animator>();
        endingTimeline.gameObject.SetActive(false);
        audio= gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Walk");
            Invoke("DelayWalk", 0.5f);
        }
    }
    void DelayWalk()
    {
        isFollow = true;
        dogHead.GetComponent<SpriteRenderer>().sortingLayerName = "Interactables-Front";


    }
    public void Attach()
    {
        isAttached=true;

    }
    void Update()
    {
        if (isFollow)
        {
            if(audio.clip != Bark)
            {
                audio.loop = false;
                audio.Stop();
                audio.clip = Bark;
                audio.Play();

            }

            player.GetComponent<LV1_PlayerController>().canSwitchLane=false;
            bool playerMoving = player.GetComponent<LV1_PlayerController>().direction !=Vector3.zero;
            anim.SetBool("Follow", playerMoving);
            transform.position = Vector3.MoveTowards(
                               transform.position,
                               player.transform.position,
                               speed * Time.deltaTime
       );
        

            if (Vector3.Distance(this.transform.position, player.transform.position) <= 0.1f && player.GetComponent<LV1_PlayerController>().inEndingGum)
            {
                anim.SetTrigger("Bend");
                isFollow = false;

            }
            player.GetComponent<LV1_PlayerController>().canSwitchLane = false;
            player.GetComponent<LV1_PlayerController>().canMoveBack = false;
        }
        if (isAttached)
        {
            player.transform.position = dogHeadBone.transform.position;
            playerShadow.SetActive(false);
            player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            player.transform.localRotation = Quaternion.identity;
            Invoke("PlayTimeline", 2f);
        }
    }
    void PlayTimeline()
    {
        endingTimeline.gameObject.SetActive(true);
        endingTimeline.Play();
    }

}
