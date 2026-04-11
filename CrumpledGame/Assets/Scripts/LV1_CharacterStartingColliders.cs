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
        player = GameObject.FindGameObjectWithTag("Player").transform;

        npcStartPositions = new Vector3[Characters.Length];
        for (int i = 0; i < Characters.Length; i++)
        {
            npcStartPositions[i] = Characters[i].transform.position;
        }
        npcOffScreenX = isMarket ? -130 : -200;
    }

    void Update()
    {

        if (previousTrigger == null) return;


        if (triggered && player.position.x < previousTrigger.transform.position.x)
        {
            // wait until NPC is off screen THEN reset
            bool allNPCsOffScreen = true;
            for (int i = 0; i < Characters.Length; i++)
            {
                Debug.Log($"{Characters[i].name} parent X: {Characters[i].transform.localPosition.x} | offScreenX: {npcOffScreenX}");

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
                    Characters[i].GetComponentInChildren<LV1_NPCs>().StopWalking();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
            for (int i = 0; i < Characters.Length; i++)
            {
                Characters[i].gameObject.GetComponentInChildren<LV1_NPCs>().StartWalking();
            }

            if (WorldBoundary != null)
            {
                WorldBoundary.SetActive(true);
            }
        }
    }
}