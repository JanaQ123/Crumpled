using UnityEngine;

public class L2Camera : MonoBehaviour
{
    public Transform player;
    public float fixedX;
    public float fixedZ;
    public float offset = 5.5f;

    void LateUpdate()
    {
        transform.position = new Vector3( fixedX, player.position.y + offset , fixedZ);
    }
}
