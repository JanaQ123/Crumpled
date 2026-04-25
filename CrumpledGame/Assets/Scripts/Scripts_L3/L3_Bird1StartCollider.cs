using UnityEngine;

public class L3_Bird1StartCollider : MonoBehaviour
{
    public GameObject bird1Controller;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bird1Controller.GetComponent<L3_Bird1Controller>().startMoving = true;
        }
    }
}
