using UnityEngine;
using UnityEngine.InputSystem;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 4f;
    float playerPosX;
    Vector3 direction;
    void Update()
    {
       PlayerMove();
    }
    void PlayerMove()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        playerPosX = transform.position.x;
        playerPosX = Mathf.Clamp(playerPosX, -6.8f, 6.8f);
        transform.position = new Vector3(playerPosX, 0f, 0f);
    }
    public void OnMove(InputValue value)
    {
        direction = new Vector3(value.Get<Vector2>().x, 0f, 0f);
    }
}
