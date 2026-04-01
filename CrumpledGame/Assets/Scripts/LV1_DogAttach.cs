using UnityEngine;

public class LV1_DogAttach : MonoBehaviour
{
    [SerializeField] LV1_DogScript dogScript;
    public void AttachPlayer()
    {
        dogScript.Attach();
    }
}
