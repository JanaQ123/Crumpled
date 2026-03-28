using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 6f;
    float playerPosX;
    Vector3 direction;
    Vector3 playerPos;    
    void Update()
    {
       PlayerMove();
    }
    void PlayerMove()
    {
        //transform.Translate(direction * Time.deltaTime * moveSpeed);
        float moveAmount = direction.x * moveSpeed * Time.deltaTime;
        playerPosX = transform.localPosition.x + moveAmount;
        playerPosX = Mathf.Clamp(playerPosX, -17.2f, 17.2f);
        transform.localPosition = new Vector3(playerPosX, transform.localPosition.y, transform.localPosition.z);
    }
    public void OnMove(InputValue value)
    {
        direction = new Vector3(value.Get<Vector2>().x, 0f, 0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Bird"))
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}
