using UnityEngine;

public class CollectLetter3D : MonoBehaviour
{
    [SerializeField] ReadingLetter letterOverlay;
    private void OnTriggerEnter3D(Collider2D collision)
    {
        
            letterOverlay.ShowLetter();
            this.gameObject.SetActive(false);
        
    }
}
