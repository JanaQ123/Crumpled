using UnityEngine;

public class L3_Bird2StartCollider : MonoBehaviour
{
    public GameObject bird2Controller;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bird2Controller.GetComponent<L3_Bird2Controller>().startMoving = true;
        }
    }
}
