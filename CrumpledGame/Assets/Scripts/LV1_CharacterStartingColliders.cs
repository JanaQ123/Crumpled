using UnityEngine;

public class LV1_CharacterStartingColliders : MonoBehaviour
{
    [SerializeField] GameObject [] Characters;
    [SerializeField] GameObject previousTrigger;
    [SerializeField] GameObject WorldBoundary;
    public bool isMarket;
    float npcOffScreenX = -200;
    bool triggered = false;
    Transform player;
    Vector3[] npcStartPositions;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerParent").transform.Find("ScenePaperBall");
        }

        npcStartPositions = new Vector3[Characters.Length];
        for (int i = 0; i < Characters.Length; i++)
        {
            npcStartPositions[i] = Characters[i].transform.position;
        }
        npcOffScreenX = isMarket ? -130 : -220;
    }

    void Update()
    {
        //if (player != null) { print("found player"); }

        if (previousTrigger == null) return;


        if (triggered && player.position.x < (previousTrigger.transform.position.x-10))
        {

            bool allNPCsOffScreen = true;
            for (int i = 0; i < Characters.Length; i++)
            {
                if (Characters[i].transform.localPosition.x > npcOffScreenX)
                {
                    allNPCsOffScreen = false;
                    break;
                }
            }

            if (allNPCsOffScreen)
            {
                triggered = false;
                for (int i = 0; i < Characters.Length; i++)
                {

                    Characters[i].transform.position = npcStartPositions[i];

                    if (Characters[i].gameObject.GetComponentInChildren<LV1_NPCs>() != null)
                    {
                        Characters[i].gameObject.GetComponentInChildren<LV1_NPCs>().StopWalking();
                    }
                    else
                    {
                        Characters[i].gameObject.GetComponentInChildren<LV1_NPCMarket>().StopWalking();
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Characters.Length; i++)
            {
                if(Characters[i].gameObject.GetComponentInChildren<LV1_NPCs>() != null){
                    Characters[i].gameObject.GetComponentInChildren<LV1_NPCs>().StartWalking(); }
                else
                {
                    Characters[i].gameObject.GetComponentInChildren<LV1_NPCMarket>().StartWalking();
                }

            }
        }

            if (WorldBoundary != null)
            {
                WorldBoundary.SetActive(true);
            }
     }
    

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

}