using UnityEngine;

public class L2LayerTriggerSwitch : MonoBehaviour
{
    public string enterLayer = "InFront";
    public string exitLayer = "Default";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Renderer>().sortingLayerName = enterLayer;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Renderer>().sortingLayerName = exitLayer;
    }
}
