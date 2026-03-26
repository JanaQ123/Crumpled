using UnityEngine;

public class L2Camera : MonoBehaviour
{
    public Transform player;
    public float fixedX;
    public float fixedZ;
    public float offset = 7;

    void LateUpdate()
    {
        transform.position = new Vector3( fixedX, player.position.y + offset , fixedZ);
    }
}
