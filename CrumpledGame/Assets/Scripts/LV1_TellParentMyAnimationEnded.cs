using Unity.VisualScripting;
using UnityEngine;

public class LV1_TellParentMyAnimationEnded : MonoBehaviour
{
    [SerializeField] GameObject parent;

    public void AnimationEnded()
    {
        parent.GetComponent<LV1_StartBallHandler>().AllowMove();
    }
}
